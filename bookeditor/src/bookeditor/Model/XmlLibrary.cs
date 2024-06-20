using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace bookeditor;

public class XmlLibrary
{
    private string rootpath;
    private readonly string[] librarieNames;
    public string DefaultLibraryName { get; set; } = "New_Library";

    public XmlLibrary(string rootpath, string[] librarieNames)
    {
        this.rootpath = rootpath;
        this.librarieNames = librarieNames;
    }

    public string? RootPath { get => rootpath; set => rootpath = value ?? string.Empty; }

    private Book[]? books;
    public Book[]? Books 
    { 
        get
        {
            this.books ??= this.GetBooks();

            return this.books;
        }
    }


    public Book GetBook(string bookName)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(Library));

            string[] libraryPaths;

            if (librarieNames.Contains("*"))
                libraryPaths = Directory.GetFiles(Path.Combine(rootpath, "*.xml"));
            else
                libraryPaths = librarieNames.Select(n => Path.Combine(rootpath, $"{n}.xml")).ToArray();

            foreach (string libraryPath in libraryPaths)
            {
                using (XmlReader reader = XmlReader.Create(libraryPath))
                {
                    Library libraryModel = (serializer.Deserialize(reader) as Library) ?? new Library();

                    return libraryModel?.Books?.FirstOrDefault(b => b.Title == bookName) ?? new Book() { Title = "" };
                }
            }
        }
        catch (System.Exception)
        {
            /// [rgR] Dodgy af how should we handle exceptions? (see tests)
            return new Book() { Title = "" };
        }

        return new Book() { Title = "" };
    }

    public Book GetFirstBook()
    {
        if (!librarieNames.Any())
            return new Book() { Title = "" };

        using (XmlReader reader = XmlReader.Create(Path.Combine(rootpath, $"{librarieNames.First()}.xml")))
        {
            var serializer = new XmlSerializer(typeof(Library));
            Library libraryModel = (serializer.Deserialize(reader) as Library) ?? new Library();

            return libraryModel?.Books?.FirstOrDefault() ?? new Book() { Title = "" };
        }
    }

    public async IAsyncEnumerable<Book> GetAllBooks()
    {
        XmlReaderSettings settings = new()
        {
            Async = true   
        };

        if (librarieNames.Length == 0)
            yield return new Book();

        var serializer = new XmlSerializer(typeof(Book));
        
        string[] libraryPaths;

        if (librarieNames.Contains("*"))
            libraryPaths = Directory.GetFiles(rootpath, "*.xml");
        else
            libraryPaths = librarieNames.Select(n => Path.Combine(rootpath, $"{n}.xml")).ToArray();

        foreach (string path in libraryPaths)
        {
            using (XmlReader reader = XmlReader.Create(path, settings))
            {
                var canRead = true;
                while (canRead)
                {
                    try 
                    {
                        canRead = await reader.ReadAsync();
                    }
                    catch (Exception e)
                    {
                        // [rgR] ignore eronous files
                    }  

                    if (canRead)
                    {
                        if (reader.Name == "book")
                        {
                            yield return (serializer.Deserialize(reader) as Book) ?? new Book();
                        }
                    }
                }           
            }
        }
    }


    private Book[]? GetBooks()
    {
        if (librarieNames.Length == 0)
            return [];

        var serializer = new XmlSerializer(typeof(Book));
        
        string[] libraryPaths;

        if (librarieNames.Contains("*"))
            libraryPaths = Directory.GetFiles(rootpath, "*.xml");
        else
            libraryPaths = librarieNames.Select(n => Path.Combine(rootpath, $"{n}.xml")).ToArray();

        List<Book> allbooks = [];

        foreach (string path in libraryPaths)
        {
            using (XmlReader reader = XmlReader.Create(path))
            {
                var canRead = true;
                while (canRead)
                {
                    try 
                    {
                        canRead = reader.Read();
                    }
                    catch (Exception)
                    {
                        // [rgR] ignore eronous files
                    }  

                    if (canRead)
                    {
                        if (reader.Name == "book")
                        {
                            var libbook = serializer.Deserialize(reader) as Book;
                            allbooks.Add(libbook ?? new Book());
                        }
                    }
                }           
            }
        }

        return [.. allbooks];
    }

    public async Task WriteBookToLibrary(Book saveBook)
    {        
        var libPath = Path.Combine(rootpath, $"{DefaultLibraryName}.xml");

        XDocument doc;

        if (File.Exists(libPath))
        {
            using(var xmlReader = XmlReader.Create(libPath, new XmlReaderSettings { Async = true  }))
            {
                doc = await XDocument.LoadAsync(xmlReader, LoadOptions.PreserveWhitespace, CancellationToken.None);
            }
        }
        else
        {
            doc = new XDocument();
        }
        
        var libraryNode = doc.Nodes()
                            .OfType<XElement>()
                            .FirstOrDefault(e => e.Name.LocalName.Equals("library", StringComparison.CurrentCultureIgnoreCase));
        
        if (libraryNode == null)
        {
            libraryNode = new XElement("library");
            doc.Add(libraryNode);         
        }

        if (string.IsNullOrEmpty(saveBook.Slug))
        {
            saveBook.Slug = Guid.NewGuid().ToString();
        }

        if (saveBook.Pages != null)
        {
            foreach (var page in saveBook.Pages.Where(p => string.IsNullOrWhiteSpace(p.Slug)))
            {
                page.Slug = Guid.NewGuid().ToString(); 
            }
        }

        var serializer = new XmlSerializer(typeof(Book));
        StringWriter xout = new StringWriter();
        var bookwriterSettings = new XmlWriterSettings { 
            Async = true, 
            NamespaceHandling = NamespaceHandling.OmitDuplicates, 
            OmitXmlDeclaration = true,
            Indent = true,
            NewLineOnAttributes = true,
            NewLineChars = "\r\n",
            IndentChars = "    ",
            NewLineHandling = NewLineHandling.Replace,
            Encoding = System.Text.Encoding.UTF8
        };

        XmlWriter bookwriter = XmlWriter.Create(xout, bookwriterSettings);
        serializer.Serialize(bookwriter, saveBook);
        var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
        var booknode = XElement.Parse(xout.ToString(), LoadOptions.PreserveWhitespace);
        
        booknode.Attributes()
            .Where( x => x.IsNamespaceDeclaration )
            .Remove();
        booknode.Descendants()
            .Attributes()
            .Where( x => x.IsNamespaceDeclaration )
            .Remove();

        var oldBooknode = doc.XPathSelectElement($"./library/book[slug = '{saveBook.Slug}']");

        if (oldBooknode == null)
        {         
            libraryNode.Add(booknode);
        }
        else
        {
            oldBooknode.Remove();
            libraryNode.Add(booknode);
        }
        
        var writerSettings = new XmlWriterSettings { 
            Async = true, 
            NamespaceHandling = NamespaceHandling.OmitDuplicates, 
            OmitXmlDeclaration = true,
            Indent = true,
            NewLineOnAttributes = true,
            NewLineChars = "\r\n",
            IndentChars = "    " 
        };

        using (XmlWriter writer = XmlWriter.Create(libPath, writerSettings))
        {
            doc.Descendants()
                .Attributes()
                .Where( x => x.IsNamespaceDeclaration )
                .Remove();
            await doc.SaveAsync(writer, CancellationToken.None);
        }        
    }

    public void WriteBookToLibrarySync(Book saveBook)
    {        
        var libPath = Path.Combine(rootpath, $"{DefaultLibraryName}.xml");

        XDocument doc;

        if (File.Exists(libPath))
        {
            var options = new FileStreamOptions();
            using(var reader = File.Open(libPath, FileMode.Open))
            {
                doc = XDocument.Load(reader, LoadOptions.PreserveWhitespace);
            }
        }
        else
        {
            doc = new XDocument();
        }
        
        var libraryNode = doc.Nodes()
                            .OfType<XElement>()
                            .FirstOrDefault(e => e.Name.LocalName.Equals("library", StringComparison.CurrentCultureIgnoreCase));
        
        if (libraryNode == null)
        {
            libraryNode = new XElement("library");
            doc.Add(libraryNode);         
        }

        if (string.IsNullOrEmpty(saveBook.Slug))
        {
            saveBook.Slug = Guid.NewGuid().ToString();
        }

        if (saveBook.Pages != null)
        {
            foreach (var page in saveBook.Pages.Where(p => string.IsNullOrWhiteSpace(p.Slug)))
            {
                page.Slug = Guid.NewGuid().ToString(); 
            }
        }

        var serializer = new XmlSerializer(typeof(Book));
        StringWriter xout = new StringWriter();
        serializer.Serialize(xout, saveBook);
        var booknode = XElement.Parse(xout.ToString().Replace("</book>", "</book>  \r\n"), LoadOptions.PreserveWhitespace);
        
        booknode.Attributes()
            .Where( x => x.IsNamespaceDeclaration )
            .Remove();
        booknode.Descendants()
            .Attributes()
            .Where( x => x.IsNamespaceDeclaration )
            .Remove();

        var oldBooknode = doc.XPathSelectElement($"./library/book[slug = '{saveBook.Slug}']");

        if (oldBooknode == null)
        {         
            libraryNode.Add(booknode);
        }
        else
        {
            oldBooknode.Remove();
            libraryNode.Add(booknode);
        }

        using (FileStream writer = File.Open(libPath, FileMode.OpenOrCreate))
        {
            doc.Descendants()
                .Attributes()
                .Where( x => x.IsNamespaceDeclaration )
                .Remove();
        }        
        doc.Save(libPath);
    }
}

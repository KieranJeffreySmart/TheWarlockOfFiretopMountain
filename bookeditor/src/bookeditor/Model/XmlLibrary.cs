using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public string? RootPath 
    { 
        get => rootpath; 
        set { rootpath = value ?? string.Empty; this.books = null; } 
    }

    private Book[]? books;
    public Book[]? Books 
    { 
        get
        {
            this.books ??= this.GetBooks();
            return this.books;
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
            if (!File.Exists(path)) continue;

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

    public void WriteBookToLibrarySync(Book saveBook)
    {        
        var libPath = Path.Combine(rootpath, $"{DefaultLibraryName}.xml");

        XDocument doc;

        if (File.Exists(libPath))
        {
            var options = new FileStreamOptions();
            using(var reader = File.Open(libPath, FileMode.Open))
            {
                try 
                {
                    doc = XDocument.Load(reader, LoadOptions.PreserveWhitespace);
                }
                catch(XmlException e)
                {
                    // [rgR] use custom exception
                    throw new Exception($"Failed to save to target library {DefaultLibraryName}");
                }
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

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

    public CommandTemplate[]? CommandTemplates => 
    [
        new CommandTemplate { Name = "Start Game", Slug = "d6c87096-e90c-4ccc-b568-2922b3601397" , Factory = () => new Option { Command = "START_GAME", Key="s", Label="Start game" } },
        new CommandTemplate { Name = "Quit Game", Slug = "8d4b8346-2b2f-4b33-90f4-dc282477bdfe", Factory = () => new Option { Command = "QUIT_GAME", Key="q", Label="Quit game" }},
        new CommandTemplate { Name = "Next Page", Slug = "ea55b045-2e64-45b8-a1df-f619ab100e86", Factory = () => new Option { Command = "NEXT_PAGE", Key="n", Label="Next page" }},
        new CommandTemplate { Name = "Previous Page", Slug = "b7c928ff-e97e-4669-a4c7-65f41c291b05", Factory = () => new Option { Command = "PREVIOUS_PAGE", Key="p", Label="Previous page" }},
        new CommandTemplate { Name = "Go to Page", Slug = "9924c2ae-e716-4075-91e7-56acd24d9a72", Factory = () => new Option 
            { 
                Command = "GOTO_PAGE", Key="g", 
                Label="Go to page",
                Arguments = [ new OptionArgument { Key = "page", Value = "1" } ]
            }}
    ];

    private Book[]? GetBooks()
    {
        if (librarieNames.Length == 0)
            return [];

        var serializer = new XmlSerializer(typeof(Library));
        
        string[] libraryPaths;

        if (librarieNames.Contains("*"))
            libraryPaths = Directory.GetFiles(rootpath, "*.xml");
        else
            libraryPaths = librarieNames.Select(n => Path.Combine(rootpath, $"{n}.xml")).ToArray();

        List<Book> allbooks = [];

        foreach (string path in libraryPaths)
        {
            if (!File.Exists(path)) continue;

            using (FileStream reader = File.OpenRead(path))
            {
                try 
                {
                    var lib = serializer.Deserialize(reader) as Library;
                    allbooks.AddRange(lib?.Books?.ToList() ?? new List<Book>());
                }
                catch (Exception e)
                {
                    // [rgR] ignore eronous files
                    var msg = e.Message;
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
                    throw new Exception($"Failed to save to target library {DefaultLibraryName}", e);
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

public class CommandTemplate
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public Func<Option>? Factory { get; set; }
}
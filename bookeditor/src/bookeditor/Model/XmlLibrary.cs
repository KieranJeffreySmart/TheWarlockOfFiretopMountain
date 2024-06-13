using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace bookeditor;

public class XmlLibrary
{
    private string rootpath;
    private readonly string[] librarieNames;

    public XmlLibrary(string rootpath, string[] librarieNames)
    {
        this.rootpath = rootpath;
        this.librarieNames = librarieNames;
    }

    public string? RootPath { get => rootpath; set => rootpath = value ?? string.Empty; }

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
        catch (System.Exception e)
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

        List<string> readers = new List<string>();
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
}

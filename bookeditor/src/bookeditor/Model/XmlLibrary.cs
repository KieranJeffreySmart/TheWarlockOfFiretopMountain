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
            foreach (string libraryName in librarieNames)
            {
                using (XmlReader reader = XmlReader.Create(Path.Combine(rootpath, $"{libraryName}.xml")))
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
        XmlReaderSettings settings = new XmlReaderSettings 
        {
            Async = true   
        };

        if (!librarieNames.Any())
            yield return new Book();


        List<XmlReader> readers = new List<XmlReader>();

        foreach (string name in librarieNames)
        {
            XmlReader reader = XmlReader.Create(Path.Combine(rootpath, $"{name}.xml"), settings);
            readers.Add(reader);
        }

        var readerenumerator = readers.GetEnumerator();

        var serializer = new XmlSerializer(typeof(Book));
        readerenumerator.MoveNext();

        while (await readerenumerator.Current.ReadAsync() || readerenumerator.MoveNext())
        {
            if (readerenumerator.Current.Name == "book")
            {
                yield return (serializer.Deserialize(readerenumerator.Current) as Book) ?? new Book();
            }
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Xml;
using System.Xml.Serialization;

namespace bookeditor;

public class XmlBookRepo
{
    private string rootpath;

    public XmlBookRepo(string rootpath)
    {
        this.rootpath = rootpath;
    }

    public Book GetBook(string libraryName, string bookName)
    {
        var serializer = new XmlSerializer(typeof(Library));

        try
        {
            using (XmlReader reader = XmlReader.Create(Path.Combine(rootpath, $"{libraryName}.xml")))
            {
                Library library = (serializer.Deserialize(reader) as Library) ?? new Library();

                return library.Books.FirstOrDefault(b => b.Name == bookName) ?? new Book() { Name = "" };
            }
        }
        catch (System.Exception)
        {
            /// rgR: Dodgy af how should we handle exceptions?
            return new Book() { Name = "" };
        }
    }
}

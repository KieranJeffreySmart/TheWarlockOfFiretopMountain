using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace bookeditor;

public class XmlLibrary
{
    private readonly string rootpath;
    private readonly string[] librarieNames;

    public XmlLibrary(string rootpath, string[] librarieNames)
    {
        this.rootpath = rootpath;
        this.librarieNames = librarieNames;
    }

    public Book GetBook(string bookName)
    {
        var serializer = new XmlSerializer(typeof(Library));

        try
        {
            foreach (string libraryName in librarieNames)
            {
                using (XmlReader reader = XmlReader.Create(Path.Combine(rootpath, $"{libraryName}.xml")))
                {
                    Library library = (serializer.Deserialize(reader) as Library) ?? new Library();

                    return library.Books.FirstOrDefault(b => b.Name == bookName) ?? new Book() { Name = "" };
                }
            }
        }
        catch (System.Exception)
        {
            /// [rgR] Dodgy af how should we handle exceptions? (see tests)
            return new Book() { Name = "" };
        }

        return new Book() { Name = "" };
    }
}

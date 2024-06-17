using System.Xml.Serialization;

namespace bookeditor;

public class Library
{
    [XmlElement("book")]
    public Book[] Books { get; set; } = [];
}
using System.Xml.Serialization;

namespace bookeditor;

[XmlRoot("library")]
public class Library
{
    [XmlElement("book")]
    public Book[] Books { get; set; } = [];
}
using System.Xml.Serialization;

namespace bookeditor;
[XmlRoot("book")]
public class Book
{
    [XmlElement("title")]
    public string Title { get; set; } = "";

    [XmlElement("page")]
    public Page[] Pages { get; set; } = [];
}

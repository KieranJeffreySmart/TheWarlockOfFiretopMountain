using System;
using System.Xml.Serialization;

namespace bookeditor;
[XmlRoot("book")]
public class Book
{
    [XmlElement("title")]
    public string Title { get; set; } = "";

    [XmlElement("page")]
    public Page[] Pages { get; set; } = [];

    [XmlElement("slug")]
    public string? Slug { get; set; }
}

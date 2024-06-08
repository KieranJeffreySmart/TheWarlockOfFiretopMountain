using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace bookeditor;

public class Book
{
    [XmlElement("name")]
    public string Name { get; set; } = "";

    [XmlElement("page")]
    public List<Page> Pages { get; set; } = new List<Page>();
}

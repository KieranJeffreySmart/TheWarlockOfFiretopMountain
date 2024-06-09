using System;
using System.Xml.Serialization;

namespace bookeditor;

public class Page
{
    [XmlElement("type")]
    public string Type { get; set; } = "";

    [XmlElement("index")]
    public int Index { get; set; } = -1;

    [XmlElement("story")]
    public Story? Story { get; set; } = null;
}

public class Story
{
    [XmlElement("text")]
    public string[]? TextCarets { get; set; } = Array.Empty<string>();

}

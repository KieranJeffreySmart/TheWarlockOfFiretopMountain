using System.Xml.Serialization;

namespace bookeditor;

public class Page
{
    [XmlElement("type")]
    public string Type { get; set; } = "";

    [XmlElement("index")]
    public int Index { get; set; } = -1;
}

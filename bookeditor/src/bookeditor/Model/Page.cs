using System.Xml.Serialization;

namespace bookeditor;

public class Page
{
    [XmlElement("type")]
    public string? PageType { get; set; } = string.Empty;

    [XmlElement("index")]
    public int? Index { get; set; } = -1;

    [XmlElement("story")]
    public Story? Story { get; set; } = null;
    
    [XmlElement("scene")]
    public Scene? Scene { get; set; } = null;

    [XmlElement("option")]
    public Option[]? Options { get; set; } = [];

    [XmlElement("slug")]
    public string? Slug { get; set; }
}

public class Option
{
    [XmlAttribute("key")]
    public string? Key { get; set; } = string.Empty;

    [XmlElement("label")]
    public string? Label { get; set; } = string.Empty;

    [XmlAttribute("command")]
    public string? Command { get; set; } = string.Empty;

    [XmlElement("arg")]
    public OptionArgument[]? Arguments { get; set; } = [];
    
    [XmlElement("outcome")]
    public Outcome[]? Outcomes { get; set; } = [];
}

public class Outcome
{
    [XmlElement("story")]
    public Story? Story { get; set; } = null;
    
    [XmlElement("scene")]
    public Scene? Scene { get; set; } = null;
 
    [XmlElement("option")]
    public Option[]? Options { get; set; } = [];

    [XmlElement("type")]
    public virtual string? OutcomeType { get; set; } = "NONE";
}


public class Story
{
    [XmlElement("caret")]
    public Caret[]? Carets { get; set; } = [];
}

public class Scene
{
    [XmlElement("caret")]
    public Caret[]? Carets { get; set; } = [];
}

public class Caret
{
    [XmlAttribute("type")]
    public string? CaretType { get; set;}
    
    [XmlText]
    public string? StringValue { get; set;}
}

public class OptionArgument 
{
    [XmlElement("key")]
    public string Key { get; set; } = string.Empty;

    [XmlElement("value")]
    public string Value { get; set; } = string.Empty;
}

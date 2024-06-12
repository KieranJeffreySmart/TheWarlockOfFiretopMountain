using System;
using System.Collections.Generic;
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
    public Option[] Options { get; set; } = [];
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
    public OptionArgument[] Arguments { get; set; } = [];
}

public class Story
{
    [XmlElement("text")]
    public string[]? TextCarets { get; set; } = [];

    [XmlElement("image")]
    public string[]? Images { get; set; } = [];
}

public class Scene
{
    [XmlElement("text")]
    public string[]? TextCarets { get; set; } = [];

    [XmlElement("image")]
    public string[]? Images { get; set; } = [];
}

public class OptionArgument 
{
    [XmlElement("key")]
    public string Key { get; set; } = string.Empty;

    [XmlElement("value")]
    public string Value { get; set; } = string.Empty;
}

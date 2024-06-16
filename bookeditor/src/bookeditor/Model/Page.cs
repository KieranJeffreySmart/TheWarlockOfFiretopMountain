using System;
using System.Collections.Generic;
using System.Linq;
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

    
    [XmlArray("outcomes")]
    [XmlArrayItem("pass", typeof(PassOutcome))]
    [XmlArrayItem("fail", typeof(FailOutcome))]
    [XmlArrayItem("win", typeof(WinOutcome))]
    [XmlArrayItem("escape", typeof(EscapeOutcome))]
    [XmlArrayItem("defeat", typeof(DefeatOutcome))]
    public Outcome[]? Outcomes { get; set; } = [];
}

public class DefeatOutcome: Outcome
{
    public new string? OutcomeType => "DEFEAT";
}

public class EscapeOutcome: Outcome
{
    public new string? OutcomeType => "ESCAPE";
}

public class WinOutcome: Outcome
{
    public new string? OutcomeType => "WIN";
}

public class FailOutcome: Outcome
{   
    public new string? OutcomeType => "FAIL";
}

public class PassOutcome : Outcome
{
    public new string? OutcomeType => "PASS";
}

public class Outcome
{
    [XmlElement("story")]
    public Story? Story { get; set; } = null;
    
    [XmlElement("scene")]
    public Scene? Scene { get; set; } = null;

    [XmlElement("option")]
    public Option[]? Options { get; set; } = [];

    public string? OutcomeType => "NONE";
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

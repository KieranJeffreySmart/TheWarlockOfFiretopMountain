using System;
using System.Collections.Generic;

namespace bookeditor;

public class Book
{
    public string Name { get; set; } = "";
    public Page[] Pages { get; set; } = Array.Empty<Page>();
}

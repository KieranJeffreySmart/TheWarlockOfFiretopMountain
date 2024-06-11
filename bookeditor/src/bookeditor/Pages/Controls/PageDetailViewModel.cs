using System;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class PageDetailViewModel : DotvvmViewModelBase
{
    public Page? Page { get; set; }

    public string StoryTextRaw => String.Join(string.Empty, Page?.Story?.TextCarets ?? Array.Empty<string>());

}
using System;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class PageItemViewModel : DotvvmViewModelBase
{
    private Page page;

    public PageItemViewModel(Page page)
    {
        this.page = page;
    }

    public string Label => $"{page?.Type} Page {page?.Index}";

    public int Index => page?.Index ?? -1;

    public string StoryTextRaw => String.Join(string.Empty, page?.Story?.TextCarets ?? Array.Empty<string>());
}
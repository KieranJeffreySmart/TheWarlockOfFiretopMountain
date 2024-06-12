using System.Collections.Generic;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class PageDetailViewModel : DotvvmViewModelBase
{
    public Page? Page { get; set; }

    public string StoryTextRaw => string.Join("", Page?.Story?.TextCarets ?? []);

    public string SceneTextRaw  => string.Join("", Page?.Scene?.TextCarets ?? []);
}
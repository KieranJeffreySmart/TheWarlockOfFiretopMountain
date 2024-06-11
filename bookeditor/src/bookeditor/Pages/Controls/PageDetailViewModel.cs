using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class PageDetailViewModel : DotvvmViewModelBase
{
    public Page? Page { get; set; }

    public string StoryTextRaw => string.Join("", Page?.Story?.TextCarets ?? []);

}
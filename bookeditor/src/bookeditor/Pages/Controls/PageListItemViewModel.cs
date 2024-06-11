using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class PageListItemViewModel : DotvvmViewModelBase
{
    public string Label => $"{Page?.Type} Page {Page?.Index}";

    public int Index => Page?.Index ?? -1;

    public Page? Page { get; set;}
}

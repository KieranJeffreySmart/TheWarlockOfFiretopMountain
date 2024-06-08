using DotVVM.Framework.ViewModel;

namespace bookeditor;

public class PageItemViewModel : DotvvmViewModelBase
{
    private Page page;

    public PageItemViewModel(Page page)
    {
        this.page = page;
    }

    public string Label => $"{page?.Type} Page {page?.Index}";
}
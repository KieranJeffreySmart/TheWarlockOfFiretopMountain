using DotVVM.Framework.ViewModel;

namespace bookeditor;

public class PageViewModel : DotvvmViewModelBase
{
    private Page page;

    public PageViewModel(Page page)
    {
        this.page = page;
    }

    public string Label => $"{page?.Type} Page {page?.Index}";
}
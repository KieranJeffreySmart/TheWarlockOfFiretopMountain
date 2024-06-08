using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;
public class DefaultViewModel : DotvvmViewModelBase
{
    public string Title { get; set; }

    public DefaultViewModel()
    {
        Title = "Hello from DotVVM!";
    }
}

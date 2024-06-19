using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class CaretDetailViewModel : DotvvmViewModelBase
{
    public Caret? Caret { get; set; }

    public CaretDetailViewModel()
    {
        this.StringValue = this.Caret?.StringValue;
    }

    public string? CaretType => this.Caret?.CaretType;

    public string? StringValue { get; set; }

    public void UpdateCaret(object? viewModel)
    {
        StringValue = Caret?.StringValue ?? "";
    }
}

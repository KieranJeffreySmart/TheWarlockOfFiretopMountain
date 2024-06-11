using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class BookDetailViewModel : DotvvmViewModelBase
{
    public Book? Book { get; set; }

    public int PageCount => Book?.Pages?.Length ?? 0;
}

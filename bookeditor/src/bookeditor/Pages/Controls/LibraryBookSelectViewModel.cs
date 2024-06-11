using DotVVM.Framework.ViewModel;

namespace bookeditor;

public class LibraryBookSelectViewModel : DotvvmViewModelBase
{
    public Book[] Books { get; set; }
    public Book SelectedBook { get; set; }
}
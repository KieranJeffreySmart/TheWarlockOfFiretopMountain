using System;
using System.Linq;
using System.Threading.Tasks;
using bookeditor.ViewModels;
using DotVVM.Framework.Utils;
using DotVVM.Framework.ViewModel;
using Microsoft.CodeAnalysis.CSharp;

namespace bookeditor;

public class BookEditorHomeViewModel : DotvvmViewModelBase
{
    public string? LibraryPath { get {return this.library?.RootPath; } set { if (this.library != null) {this.library.RootPath = value; } } }

    private readonly XmlLibrary? library;
    private readonly InMemoryNotificationsQueue? notificationsQueue;

    private Book[] books = Array.Empty<Book>();

    public BookEditorHomeViewModel(XmlLibrary library, InMemoryNotificationsQueue notificationsQueue)
    {
        this.library = library;
        this.notificationsQueue = notificationsQueue;
    }

    public override async Task PreRender() 
    {
        await this.OpenLibrary();
        await base.PreRender();
    }

    public IQueryable<Book>? Books => books.AsQueryable();

    public LibraryBookSelectViewModel LibraryBookSelector { get; } = new LibraryBookSelectViewModel();

    public Book? SelectedBook { get; set; }

    public BookPageListViewModel SelectedBookPageList { get; } = new BookPageListViewModel();

    public Page? SelectedPage { get; set; }

    public PageDetailViewModel? SelectedPageDetail { get; } = new PageDetailViewModel();
    
    public async Task OpenLibrary()
    {
        if ( library != null)
        {
            await foreach (var item in library.GetAllBooks())
            {
                this.books.Append(item);
            }

            if (this.books.Length == 0)
            {
                this.RaiseNotification($"{(this.books.Length > 0 ? "no" : this.books.Length)} books were found");
            }
        }
    }

    private void RaiseNotification(string notification)
    {
        this.notificationsQueue?.Push(notification);
    }

    public void OpenSelectedBook()
    {
        throw new NotImplementedException();
    }
}

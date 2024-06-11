using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

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

    public Book? SelectedBook { get; set; }

    public BookDetailViewModel SelectedBookDetails { get; } = new BookDetailViewModel();
    public void UpdateSelectedViewModels()
    {
        this.SelectedBookDetails.Book = SelectedBook;
        this.SelectedPageDetails.Page = this.SelectedPage;
    }

    public Page? SelectedPage { get; set; }

    public PageDetailViewModel SelectedPageDetails { get; } = new PageDetailViewModel();
    
    public async Task OpenLibrary()
    {
        if ( library != null)
        {
            List<Book> asyncBooks = new List<Book>();
            await foreach (var book in library.GetAllBooks())
            {
                asyncBooks.Add(book);
            }

            this.books = [.. asyncBooks];

            this.RaiseNotification($"{(this.books.Length == 0 ? "no" : this.books.Length)} book{(this.books.Length != 1 ? "s were" : " was")} found");
        }
    } 

    private void RaiseNotification(string notification)
    {
        this.notificationsQueue?.Push(notification);
    }
}

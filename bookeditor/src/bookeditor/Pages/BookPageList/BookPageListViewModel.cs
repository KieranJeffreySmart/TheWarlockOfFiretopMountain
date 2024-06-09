using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class BookPageListViewModel : DotvvmViewModelBase
{
    private readonly XmlLibrary library;
    private readonly InMemoryNotificationsQueue notificationsQueue;

    public BookPageListViewModel(XmlLibrary library, InMemoryNotificationsQueue notificationsQueue)
    {
        this.library = library;
        this.notificationsQueue = notificationsQueue;
    }

    public string SelectedBookTitle { get; set; } = string.Empty;

    public ICollection<PageItemViewModel> Pages { get; private set; } = Array.Empty<PageItemViewModel>();

    public PageItemViewModel? SelectedValue { get; set; } = null;

    public string? LibraryPath { get {return this.library?.RootPath; } set { if (this.library != null) {this.library.RootPath = value; } } }

    public string BookTitle { get; private set; } = "";

    public void OpenBook(string testBook)
    {
        Book book = library.GetBook(testBook);    
        SetBookProperties(book);

        // [rgR] I like this but probably should break it down
        this.RaiseNotification($"The book {(string.IsNullOrWhiteSpace(book.Title) ? "was not found" : $"[{book.Title}] opened {(!book.Pages.Any() ? "but is empty" : $"with {book.Pages.Count} page{(book.Pages.Count != 1 ? "s" : "")}")}")}");
    }

    public void OpenSelectedBook()
    {
        if (string.IsNullOrWhiteSpace(SelectedBookTitle))
        {    
            Book book = library.GetFirstBook();      
            SetBookProperties(book);
        }
        else
        {
            OpenBook(SelectedBookTitle);
        }
    }

    private void SetBookProperties(Book book)
    {
        this.Pages = book.Pages.Select(p => new PageItemViewModel(p)).ToArray();
        this.BookTitle = SelectedBookTitle = book.Title; 
    }

    private void RaiseNotification(string notification)
    {
        this.notificationsQueue.Push(notification);
    }
}
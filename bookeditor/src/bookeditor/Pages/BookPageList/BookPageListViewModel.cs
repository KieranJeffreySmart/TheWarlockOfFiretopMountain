using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace bookeditor;

public class BookPageListViewModel : DotvvmViewModelBase
{
    private XmlLibrary library;
    private InMemoryNotificationsQueue notificationsQueue;

    public BookPageListViewModel(XmlLibrary library, InMemoryNotificationsQueue notificationsQueue)
    {
        this.library = library;
        this.notificationsQueue = notificationsQueue;
    }

    public ICollection<PageItemViewModel> Pages { get; internal set; } = Array.Empty<PageItemViewModel>();

    public void OpenBook(string testBook)
    {
        Book book = library.GetBook(testBook);
        this.Pages = book.Pages.Select(p => new PageItemViewModel(p)).ToArray();

        // [rgR] I like this but probably should break it down
        this.RaiseNotification($"The book {(book.Name == "" ? "was not found" : $"[{book.Name}] opened {(book.Pages.Any() ? $"with {book.Pages.Count} page{(book.Pages.Count != 1 ? "s" : "")}" : "but is empty")}")}");
    }

    private void RaiseNotification(string notification)
    {
        this.notificationsQueue.Push(notification);
    }
}
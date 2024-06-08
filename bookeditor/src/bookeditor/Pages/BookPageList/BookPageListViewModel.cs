using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace bookeditor;

public class BookPageListViewModel : DotvvmViewModelBase
{
    private XmlBookRepo testDataRepository;
    private InMemoryNotificationsQueue notificationsQueue;

    public BookPageListViewModel(XmlBookRepo testDataRepository, InMemoryNotificationsQueue notificationsQueue)
    {
        this.testDataRepository = testDataRepository;
        this.notificationsQueue = notificationsQueue;
    }

    public ICollection<PageItemViewModel> Pages { get; internal set; } = Array.Empty<PageItemViewModel>();

    public void OpenBook(string library, string testBook)
    {
        Book book = testDataRepository.GetBook(library, testBook);
        this.Pages = book.Pages.Select(p => new PageItemViewModel(p)).ToArray();

        // [rgR] I like this but probably should break it down
        this.RaiseNotification($"The book {(book.Name == "" ? "was not found" : $"[{book.Name}] opened {(book.Pages.Any() ? $"with {book.Pages.Count} page{(book.Pages.Count != 1 ? "s" : "")}" : "but is empty")}")}");
    }

    private void RaiseNotification(string notification)
    {
        this.notificationsQueue.Push(notification);
    }
}
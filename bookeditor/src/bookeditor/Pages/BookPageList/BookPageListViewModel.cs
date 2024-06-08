﻿using System;
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

    public ICollection<PageViewModel> Pages { get; internal set; } = Array.Empty<PageViewModel>();
    public ICollection<NotificationViewModel> Notifications { get; set; } = Array.Empty<NotificationViewModel>();

    public void OpenBook(string library, string testBook)
    {
        Book book = testDataRepository.GetBook(library, testBook);
        this.Pages = book.Pages.Select(p => new PageViewModel(p)).ToArray();

        this.RaiseNotification($"The book {(book.Name == "" ? "was not found" : $"[{book.Name}] opened {(book.Pages.Any() ? $"with {book.Pages.Count} page{(book.Pages.Count != 1 ? "s" : "")}" : "but is empty")}")}");
    }

    private void RaiseNotification(string notification)
    {
        this.notificationsQueue.Push(notification);
    }
}

public class NotificationViewModel : DotvvmViewModelBase
{
    public string Description { get; } = "Empty";
}

public class PageViewModel : DotvvmViewModelBase
{
    private Page page;

    public PageViewModel(Page page)
    {
        this.page = page;
    }

    public string Label => $"{page?.Type} Page {page?.Index}";
}
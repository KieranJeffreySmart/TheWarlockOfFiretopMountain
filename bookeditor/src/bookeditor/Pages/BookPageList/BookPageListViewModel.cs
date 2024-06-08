using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DotVVM.Framework.ViewModel;

namespace bookeditor;

public class BookPageListViewModel : DotvvmViewModelBase
{
    private XmlBookRepo testDataRepository;

    public BookPageListViewModel(XmlBookRepo testDataRepository)
    {
        this.testDataRepository = testDataRepository;
    }

    public ICollection<PageViewModel> Pages { get; set; } = Array.Empty<PageViewModel>();
    public ICollection<NotificationViewModel> Notifications { get; set; } = Array.Empty<NotificationViewModel>();

    public void OpenBook(string testBook)
    {
        throw new NotImplementedException();
    }
}

public class NotificationViewModel : DotvvmViewModelBase
{
    public string Description { get; } = "Empty";
}

public class PageViewModel : DotvvmViewModelBase
{
    public string Label { get; } = "Empty";
}
using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace bookeditor;

public class BookPageListViewModel : DotvvmViewModelBase
{
    private XmlBookRepo testDataRepository;

    public BookPageListViewModel(XmlBookRepo testDataRepository)
    {
        this.testDataRepository = testDataRepository;
    }

    public ICollection<PageViewModel> Pages { get; internal set; } = Array.Empty<PageViewModel>();
    public ICollection<NotificationViewModel> Notifications { get; set; } = Array.Empty<NotificationViewModel>();

    public void OpenBook(string library, string testBook)
    {
        Book book = testDataRepository.GetBook(library, testBook);
        this.Pages = book.Pages.Select(p => new PageViewModel(p)).ToArray();
    }

    private void RaiseNotification()
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
    private Page page;

    public PageViewModel(Page page)
    {
        this.page = page;
    }

    public string Label { get; } = "Empty";
}
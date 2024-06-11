using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class BookPageListViewModel : DotvvmViewModelBase
{
    public Book? Book { get; set; }
    
    public IQueryable<PageListItemViewModel> Pages => 
        this.Book?.Pages.Select(p => new PageListItemViewModel { Page = p }).AsQueryable() 
        ?? Array.Empty<PageListItemViewModel>().AsQueryable();

    public Page? SelectedPage { get; set; } = null;
}
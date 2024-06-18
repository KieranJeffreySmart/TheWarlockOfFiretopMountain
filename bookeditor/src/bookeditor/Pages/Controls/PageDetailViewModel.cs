using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class PageDetailViewModel : DotvvmViewModelBase
{
    public Page? Page { get; private set; }

    public PageDetailViewModel(Page? page)
    {
        Page = page;
        this.optionViewModels = CreateViewModels(page);
    }

    private OptionDetailViewModel[]? CreateViewModels(Page? source_page)
    {
        return source_page?.Options?.Select(o => new OptionDetailViewModel(o))?.ToArray() ?? Array.Empty<OptionDetailViewModel>();
    }

    private OptionDetailViewModel[]? optionViewModels = Array.Empty<OptionDetailViewModel>();
    public bool HasOptions => (optionViewModels?.Length ?? 0) > 0;

    public OptionDetailViewModel[]? OptionViewModels => this.optionViewModels;
    public Caret? EditingCaret { get; private set; }
    
    public void EditCaret(Caret? caret)
    {
        this.EditingCaret = caret;
    }
}
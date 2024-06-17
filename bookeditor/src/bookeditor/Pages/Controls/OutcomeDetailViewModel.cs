using System;
using System.Linq;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class OutcomeDetailViewModel: DotvvmViewModelBase
{
    public Outcome? Outcome { get; private set; }
    public bool HasOptions => (optionViewModels?.Length ?? 0) > 0;

    public OutcomeDetailViewModel(Outcome outcome)
    {
        this.Outcome = outcome;
        this.optionViewModels = CreateViewModels(outcome);
    }

    private OptionDetailViewModel[]? CreateViewModels(Outcome? source_outcome)
    {
        return source_outcome?.Options?.Select(o => new OptionDetailViewModel(o))?.ToArray() ?? [];
    }
    
    private OptionDetailViewModel[]? optionViewModels = [];

    public OptionDetailViewModel[]? OptionViewModels => this.optionViewModels;
}



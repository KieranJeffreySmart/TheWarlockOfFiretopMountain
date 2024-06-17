using System;
using System.Linq;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class OptionDetailViewModel : DotvvmViewModelBase
{
    public Option? Option { get; private set; } 

    public bool HasArguments => (Option?.Arguments?.Length ?? 0) > 0;
    public bool HasOutcomes => (outcomeViewModels?.Length ?? 0) > 0;

    private OutcomeDetailViewModel[]? CreateViewModels(Option? source_option)
    {
        return source_option?.Outcomes?.Select(outcome => new OutcomeDetailViewModel(outcome))?.ToArray() ?? [];
    }
    
    private OutcomeDetailViewModel[]? outcomeViewModels = [];

    public OptionDetailViewModel(Option option)
    {
        this.Option = option;
        this.outcomeViewModels = CreateViewModels(option);
    }

    public OutcomeDetailViewModel[]? OutcomeViewModels => outcomeViewModels;    
}
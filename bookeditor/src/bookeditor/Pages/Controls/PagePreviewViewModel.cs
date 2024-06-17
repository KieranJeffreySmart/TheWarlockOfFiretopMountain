using System.Linq;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class PagePreviewViewModel : DotvvmViewModelBase
{
    public Page? Page { get; set; }

    public string StoryTextRaw => string.Join("", Page?.Story?.Carets?.Where(c => c.CaretType == "text").Select(c => c.StringValue) ?? []);

    public string SceneTextRaw  => string.Join("", Page?.Scene?.Carets?.Where(c => c.CaretType == "text").Select(c => c.StringValue) ?? []);
}
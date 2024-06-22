using System.Linq;
using DotVVM.Framework.ViewModel;

namespace bookeditor.ViewModels;

public class PagePreviewViewModel : DotvvmViewModelBase
{
    public Page? Page { get; set; }

    public string StoryTextRaw => string.Join("", Page?.Story?.Blocks?.Where(c => c.BlockType == "text").Select(c => c.StringValue) ?? []);

    public string SceneTextRaw  => string.Join("", Page?.Scene?.Blocks?.Where(c => c.BlockType == "text").Select(c => c.StringValue) ?? []);
}
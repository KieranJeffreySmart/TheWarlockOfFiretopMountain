using bookeditor.ViewModels;

namespace bookeditor.test;

public static class ViewModelArrangement
{

    public static void OpenPage(BookEditorHomeViewModel homePage, string bookSlug, Func<Page, bool>? predicate = null)
    {
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Slug == bookSlug);
        homePage.SelectBook(book);        
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Pages);
        Assert.NotEmpty(homePage.SelectedBook.Pages);
        var page = predicate != null ? homePage.SelectedBook.Pages.First(predicate) : homePage.SelectedBook.Pages.First();
        Assert.NotNull(page);
        
        homePage.SelectPage(page);
    }

    public static BookEditorHomeViewModel CreateLibrary(string rootPath, string[] libraryNames, string defaultLibrary = "New_Test_Library")
    {
        var library = new XmlLibrary(rootPath, libraryNames)
        {
            DefaultLibraryName = defaultLibrary
        };

        var cache = new EditorStateCache();

        return new BookEditorHomeViewModel(library, cache);
    }
}
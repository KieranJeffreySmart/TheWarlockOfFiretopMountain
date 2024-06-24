using System.Text.Json;
using bookeditor.ViewModels;

namespace bookeditor.test;

public static class ViewModelArrangement
{

    public static void OpenPage(BookEditorHomeViewModel homePage, string bookSlug, Func<Page, bool>? predicate = null)
    {
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Slug == bookSlug);
        Assert.NotNull(book);
        var jsonString = JsonSerializer.Serialize<Book>(book);
        var bookdto = JsonSerializer.Deserialize<Book>(jsonString);
        homePage.SelectBook(bookdto);
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Pages);
        Assert.NotEmpty(homePage.SelectedBook.Pages);
        var page = predicate != null ? homePage.SelectedBook.Pages.First(predicate) : homePage.SelectedBook.Pages.First();
        Assert.NotNull(page);
        jsonString = JsonSerializer.Serialize<Page>(page);
        var pagedto = JsonSerializer.Deserialize<Page>(jsonString);
        homePage.SelectPage(pagedto);
    }

    public static BookEditorHomeViewModel CreateHomePageVM(string rootPath, string[] libraryNames, string defaultLibrary = "New_Test_Library")
    {
        var library = new XmlLibrary(rootPath, libraryNames)
        {
            DefaultLibraryName = defaultLibrary
        };

        var cache = new EditorStateCache();

        return new BookEditorHomeViewModel(library, cache);
    }
}
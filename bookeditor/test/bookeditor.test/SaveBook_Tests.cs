using bookeditor.ViewModels;

namespace bookeditor.test;

public class SaveBook_Tests
{
    [Fact]
    public async Task SaveModifiedBook()
    {

        // Given I have a library
        string[] libraryNames = ["Books_With_Stories"];
        var library = new XmlLibrary("../../../TestData", libraryNames);
        var cache = new EditorStateCache();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new(library, cache);
        Assert.NotNull(homePage);

        // given I have a book
        var bookTitle = "Single caret story";
        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Title == bookTitle);

        // given I have a page with some story
        Assert.NotNull(book);
        Assert.NotNull(book.Pages);
        Assert.Single(book.Pages);
        var page = book.Pages.First();
        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Carets);
        Assert.Single(page.Story.Carets);
        var caret = page.Story.Carets.First();
        Assert.Equal("My single caret", caret.StringValue);

        // when I change the story
        caret.StringValue = "new test value";

        // when I save to file
        await homePage.SaveToFile();

        // when I re-open the app
        library = new XmlLibrary("../../../TestData", libraryNames);
        cache = new EditorStateCache();
        homePage = new(library, cache);
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        book = homePage.Books.First(b => b.Title == bookTitle);

        // then the changes should be displayed
        Assert.NotNull(book);
        Assert.NotNull(book.Pages);
        Assert.Single(book.Pages);
        page = book.Pages.First();
        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Carets);
        Assert.Single(page.Story.Carets);
        caret = page.Story.Carets.First();
        Assert.Equal("new test value", caret.StringValue);
    }
}
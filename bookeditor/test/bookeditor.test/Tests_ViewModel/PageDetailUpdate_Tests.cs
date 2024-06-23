using bookeditor.ViewModels;

namespace bookeditor.test;

public class PageDetailUpdate_Tests
{    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/ChangeStoryBlockdBook.xml", "../../../TestData/Books_With_Stories.xml")]
    public void ChangeStoryBlockdBook()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "ChangeStoryBlockdBook";
        var library = new XmlLibrary(rootPath, [defaultLibrary]);
        library.DefaultLibraryName = defaultLibrary;
        var fullPath = Path.Combine(rootPath, $"{defaultLibrary}.xml");
        
        // given I have a book with a title
        var bookSlug = "test-book1-slug";
        var pageSlug = "test-page1-slug";
        
        var cache = new EditorStateCache();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new(library, cache);
        Assert.NotNull(homePage);

        // given I have a book
        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Slug == bookSlug);
        homePage.SelectedBook = book;

        // given I have a page with some story
        Assert.NotNull(book);
        Assert.NotNull(book.Pages);
        Assert.NotEmpty(book.Pages);
        var page = book.Pages.First(p => p.Slug == pageSlug);
        homePage.SelectedPage = page;
        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Blocks);
        Assert.NotEmpty(page.Story.Blocks);
        var block = page.Story.Blocks.First();
        Assert.Equal("test-page3", block.StringValue);

        // when I change the story
        block.StringValue = "new test value";

        // when I save to file
        homePage.SaveToFile();

        // when I re-open the app
        library = new XmlLibrary("../../../TestData", [library.DefaultLibraryName]);
        cache = new EditorStateCache();
        homePage = new(library, cache);
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        book = homePage.Books.First(b => b.Slug == bookSlug);

        // then the changes should be displayed
        Assert.NotNull(book);
        Assert.NotNull(book.Pages);
        Assert.NotEmpty(book.Pages);
        page = book.Pages.First();
        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Blocks);
        Assert.NotEmpty(page.Story.Blocks);
        block = page.Story.Blocks.First();
        Assert.Equal("new test value", block.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/ChangeSceneBlockdBook.xml", "../../../TestData/Books_With_Scenes.xml")]
    public void ChangeSceneBlockdBook()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "ChangeSceneBlockdBook";
        var library = new XmlLibrary(rootPath, [defaultLibrary]);
        library.DefaultLibraryName = defaultLibrary;
        var fullPath = Path.Combine(rootPath, $"{defaultLibrary}.xml");
        
        // given I have a book with a title
        var slug = "test-book2-slug";
        
        var cache = new EditorStateCache();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new(library, cache);
        Assert.NotNull(homePage);

        // given I have a book
        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Slug == slug);
        homePage.SelectedBook = book;

        // given I have a page with a scene
        Assert.NotNull(book);
        Assert.NotNull(book.Pages);
        Assert.NotEmpty(book.Pages);
        var page = book.Pages.First();
        homePage.SelectedPage = page;
        Assert.NotNull(page.Scene);
        Assert.NotNull(page.Scene.Blocks);
        Assert.NotEmpty(page.Scene.Blocks);
        var block = page.Scene.Blocks.First();
        Assert.Equal("test-page3", block.StringValue);

        // when I change the scene
        block.StringValue = "new test value";

        // when I save to file
        homePage.SaveToFile();

        // when I re-open the app
        library = new XmlLibrary("../../../TestData", [library.DefaultLibraryName]);
        cache = new EditorStateCache();
        homePage = new(library, cache);
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        book = homePage.Books.First(b => b.Slug == slug);


        // then the changes should be displayed
        Assert.NotNull(book);
        Assert.NotNull(book.Pages);
        Assert.NotEmpty(book.Pages);
        page = book.Pages.First();
        Assert.NotNull(page.Scene);
        Assert.NotNull(page.Scene.Blocks);
        Assert.NotEmpty(page.Scene.Blocks);
        block = page.Scene.Blocks.First();
        Assert.Equal("new test value", block.StringValue);
    }
}
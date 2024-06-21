using bookeditor.ViewModels;

namespace bookeditor.test;

public class CaretList_Tests
{
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendCaretToEmptyScene.xml", "../../../TestData/Books_With_Scenes.xml")]
    public void AppendCaretToEmptyScene()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendCaretToEmptyScene";
        var library = new XmlLibrary(rootPath, [defaultLibrary]);
        library.DefaultLibraryName = defaultLibrary;
        var fullPath = Path.Combine(rootPath, $"{defaultLibrary}.xml");

        // given I have opened a book with no scene
        var slug = "0f19667e-283e-46df-b458-df77cdefc4bb";
        var title = "No scene";
        
        var cache = new EditorStateCache();
        BookEditorHomeViewModel homePage = new(library, cache);
        Assert.NotNull(homePage);

        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Slug == slug);
        Assert.Equal(title, book.Title);
        homePage.SelectedBook = book;        
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Pages);
        Assert.NotEmpty(homePage.SelectedBook.Pages);
        var page = homePage.SelectedBook.Pages.First();
        homePage.SelectedPage = page;
        Assert.Null(homePage.SelectedPage.Scene);

        // when I append a new caret to the scene
        homePage.AppendSceneCaret();

        // then that scene is displayed with only the new caret
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Carets);
        Assert.NotEmpty(homePage.SelectedPage.Scene.Carets);
        var caret = homePage.SelectedPage.Scene.Carets.First();
        Assert.Equal("text", caret.CaretType);
        Assert.Equal("", caret.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendSceneCaret.xml", "../../../TestData/Books_With_Scenes.xml")]
    public void AppendSceneCaret()
    {
        // when I append a caret to the scene
        // then that scene is displayed with the two carets in order

        
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendSceneCaret";
        var library = new XmlLibrary(rootPath, [defaultLibrary]);
        library.DefaultLibraryName = defaultLibrary;
        var fullPath = Path.Combine(rootPath, $"{defaultLibrary}.xml");

        // given I have a book with a scene that has a caret
        var slug = "596d5e6a-3cfb-41af-b855-8bafa5a632f3";
        var title = "Single caret scene";
        
        var cache = new EditorStateCache();
        BookEditorHomeViewModel homePage = new(library, cache);
        Assert.NotNull(homePage);

        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Slug == slug);
        Assert.Equal(title, book.Title);
        homePage.SelectedBook = book;        
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Pages);
        Assert.NotEmpty(homePage.SelectedBook.Pages);
        var page = homePage.SelectedBook.Pages.First();
        homePage.SelectedPage = page;
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Carets);
        Assert.Single(homePage.SelectedPage.Scene.Carets);
        var firstcaret = homePage.SelectedPage.Scene.Carets.First();
        Assert.Equal("text", firstcaret.CaretType);
        Assert.Equal("My single caret", firstcaret.StringValue);

        // when I append a new caret to the scene
        homePage.AppendSceneCaret();

        // then that scene is displayed with only the new caret
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Carets);
        Assert.NotEmpty(homePage.SelectedPage.Scene.Carets);
        firstcaret = homePage.SelectedPage.Scene.Carets[0];
        Assert.Equal("text", firstcaret.CaretType);
        Assert.Equal("My single caret", firstcaret.StringValue);
        firstcaret = homePage.SelectedPage.Scene.Carets[1];
        Assert.Equal("text", firstcaret.CaretType);
        Assert.Equal("", firstcaret.StringValue);
    }
    
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendToEmptyStoryCaret.xml", "../../../TestData/Books_With_Stories.xml")]
    public void AppendCaretToEmptyStory()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendToEmptyStoryCaret";
        var library = new XmlLibrary(rootPath, [defaultLibrary]);
        library.DefaultLibraryName = defaultLibrary;
        var fullPath = Path.Combine(rootPath, $"{defaultLibrary}.xml");

        // given I have opened a book with no story
        var slug = "0f19667e-283e-46df-b458-df77cdefc4bb";
        var title = "No story";
        
        var cache = new EditorStateCache();
        BookEditorHomeViewModel homePage = new(library, cache);
        Assert.NotNull(homePage);

        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Slug == slug);
        Assert.Equal(title, book.Title);
        homePage.SelectedBook = book;        
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Pages);
        Assert.NotEmpty(homePage.SelectedBook.Pages);
        var page = homePage.SelectedBook.Pages.First();
        homePage.SelectedPage = page;
        Assert.Null(homePage.SelectedPage.Story);

        // when I append a new caret to the story
        homePage.AppendStoryCaret();

        // then that story is displayed with only the new caret
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Carets);
        Assert.NotEmpty(homePage.SelectedPage.Story.Carets);
        var caret = homePage.SelectedPage.Story.Carets.First();
        Assert.Equal("text", caret.CaretType);
        Assert.Equal("", caret.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendStoryCaret.xml", "../../../TestData/Books_With_Stories.xml")]
    public void AppendStoryCaret()
    {
        // when I append a caret to the story
        // then that story is displayed with the two carets in order

        
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendStoryCaret";
        var library = new XmlLibrary(rootPath, [defaultLibrary]);
        library.DefaultLibraryName = defaultLibrary;
        var fullPath = Path.Combine(rootPath, $"{defaultLibrary}.xml");

        // given I have a book with a story that has a caret
        var slug = "596d5e6a-3cfb-41af-b855-8bafa5a632f3";
        var title = "Single caret story";
        
        var cache = new EditorStateCache();
        BookEditorHomeViewModel homePage = new(library, cache);
        Assert.NotNull(homePage);

        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Slug == slug);
        Assert.Equal(title, book.Title);
        homePage.SelectedBook = book;        
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Pages);
        Assert.NotEmpty(homePage.SelectedBook.Pages);
        var page = homePage.SelectedBook.Pages.First();
        homePage.SelectedPage = page;
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Carets);
        Assert.Single(homePage.SelectedPage.Story.Carets);
        var firstcaret = homePage.SelectedPage.Story.Carets.First();
        Assert.Equal("text", firstcaret.CaretType);
        Assert.Equal("My single caret", firstcaret.StringValue);

        // when I append a new caret to the story
        homePage.AppendStoryCaret();

        // then that story is displayed with only the new caret
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Carets);
        Assert.NotEmpty(homePage.SelectedPage.Story.Carets);
        firstcaret = homePage.SelectedPage.Story.Carets[0];
        Assert.Equal("text", firstcaret.CaretType);
        Assert.Equal("My single caret", firstcaret.StringValue);
        firstcaret = homePage.SelectedPage.Story.Carets[1];
        Assert.Equal("text", firstcaret.CaretType);
        Assert.Equal("", firstcaret.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/InsertStoryCaret.xml", "../../../TestData/Books_With_Stories.xml")]
    public void InsertStoryCaret()
    {
        // given I have a book with a story that has two carets
        // when I insert a caret after the first
        // then that story is displayed with the three carets in order
        throw new NotImplementedException();
    }
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/RemoveStoryCaret.xml", "../../../TestData/Books_With_Stories.xml")]
    public void RemoveStoryCaret()
    {
        // given I have a book with a story that has two caret
        // when I delete the second caret
        // then that story is displayed with only the first caret
        throw new NotImplementedException();
    }
}
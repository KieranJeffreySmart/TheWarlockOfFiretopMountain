using bookeditor.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace bookeditor.test;

public class BookEditorHomePage_LandingTests
{
    [Fact]
    public async Task OpenningHomePageWithNoBooks()
    {
        // Given I have a library
        var libraryName = "Empty_Library";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // given I have a home page 
        BookEditorHomeViewModel homePage = new(library, new EditorStateCache());

        // when I open the home page
        await homePage.Init();

        // then a book selector is displayed with no books
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        Assert.Empty(homePage.Books);
    }

    [Fact]
    public async Task OpenningHomePageWithSingleBook()
    {
        // Given I have a library
        var libraryName = "Warlock_Of_Firetop_Mountain";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // given I have a home page 
        BookEditorHomeViewModel homePage = new BookEditorHomeViewModel(library, new EditorStateCache());

        // when I open the home page
        await homePage.Init();

        // then a book selector is displayed with 1 book
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        Assert.Single(homePage.Books);
        Assert.Equal("Warlock of Firetop Mountain", homePage.Books.First().Title);

    }
    
    [Fact]
    public async Task OpenningHomePageWithManyBooks()
    {
        // Given I have a library
        string[] libraryNames = ["Warlock_Of_Firetop_Mountain", "Books_With_Pages"];
        var library = new XmlLibrary("../../../TestData", libraryNames);

        // given I have a home page 
        BookEditorHomeViewModel homePage = new(library, new EditorStateCache());

        // when I open the home page
        await homePage.Init();

        // then a book selector is displayed with 1 book
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        Assert.Equal(8, homePage.Books.Count());
        
    }
    
    [Fact]
    public async Task ReOpenningHomePageWithSelectedBookAndPage()
    {
        // Given I have a library
        string[] libraryNames = ["Warlock_Of_Firetop_Mountain", "Books_With_Pages"];
        var library = new XmlLibrary("../../../TestData", libraryNames);
        var cache = new EditorStateCache();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new(library, cache);

        // when I open the home page
        Assert.NotNull(homePage);
        await homePage.Init();
        
        // when I select the first book
        Assert.NotNull(homePage.Books);
        homePage.SelectedBook = homePage.Books.First();
        homePage.UpdateSelectedBook();

        // when I select the first page
        homePage.SelectedPage = homePage.SelectedBook.Pages.First();
        homePage.UpdateSelectedPage();

        // then the page selected details are displayed
        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Carets);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal("Intro", homePage.SelectedPage.PageType);
        Assert.Equal(1, homePage.SelectedPage.Index);
        Assert.Equal(2, homePage.SelectedPage.Story.Carets.Length);
        Assert.Equal(2, homePage.SelectedPage.Options.Length);

        // when I refresh the page
        homePage = new(library, cache);

        // then I the selected page details to be displayed
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Title = "The Warlock of Firetop Mountain");
        Assert.NotNull(homePage.SelectedBookDetails.Book);
        Assert.NotNull(homePage.SelectedBookDetails.Book.Title = "The Warlock of Firetop Mountain");
        Assert.NotNull(homePage.SelectedPage);
        
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Carets);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal("Intro", homePage.SelectedPage.PageType);
        Assert.Equal(1, homePage.SelectedPage.Index);
        Assert.Equal(2, homePage.SelectedPage.Story.Carets.Length);
        Assert.Equal(2, homePage.SelectedPage.Options.Length);

    }

    [Fact]
    public async Task ReOpenningHomePageAfterChangingSelectedBookAndPage()
    {
        // Given I have a library
        string[] libraryNames = ["Warlock_Of_Firetop_Mountain", "Books_With_Pages"];
        var library = new XmlLibrary("../../../TestData", libraryNames);
        var cache = new EditorStateCache();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new(library, cache);

        // when I open the home page
        Assert.NotNull(homePage);
        await homePage.Init();
        
        // when I select the first book
        Assert.NotNull(homePage.Books);
        homePage.SelectedBook = homePage.Books.First();
        homePage.UpdateSelectedBook();

        // when I select the first page
        homePage.SelectedPage = homePage.SelectedBook.Pages.First();
        homePage.UpdateSelectedPage();

        // then the page selected details are displayed
        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Carets);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal("Intro", homePage.SelectedPage.PageType);
        Assert.Equal(1, homePage.SelectedPage.Index);
        Assert.Equal(2, homePage.SelectedPage.Story.Carets.Length);
        Assert.Equal(2, homePage.SelectedPage.Options.Length);
        
        // when I select the last book
        Assert.NotNull(homePage.Books);
        homePage.SelectedBook = homePage.Books.Last();
        homePage.UpdateSelectedBook();

        // when I refresh the page
        homePage = new(library, cache);


        // then I the selected page details to be displayed
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Title = "The Warlock of Firetop Mountain");
        Assert.NotNull(homePage.SelectedBookDetails.Book);
        Assert.NotNull(homePage.SelectedBookDetails.Book.Title = "The Warlock of Firetop Mountain");
        Assert.Null(homePage.SelectedPage);
    }
}
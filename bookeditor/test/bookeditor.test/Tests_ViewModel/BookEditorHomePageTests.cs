using bookeditor.ViewModels;

namespace bookeditor.test;

public class BookEditorHomePage_LandingTests
{
    [Fact]
    public void OpenningHomePageWithNoBooks()
    {
        // Given I have a library
        var libraryName = "Empty_Library";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // when I open the home page
        BookEditorHomeViewModel homePage = new(library, new EditorStateCache());

        // then a book selector is displayed with no books
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        Assert.Empty(homePage.Books);
    }

    [Fact]
    public void OpenningHomePageWithSingleBook()
    {
        // Given I have a library
        var libraryName = "Warlock_Of_Firetop_Mountain";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // given I have a home page 

        // when I open the home page
        BookEditorHomeViewModel homePage = new BookEditorHomeViewModel(library, new EditorStateCache());

        // then a book selector is displayed with 1 book
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        Assert.Single(homePage.Books);
        Assert.Equal("Warlock of Firetop Mountain", homePage.Books.First().Title);

    }
    
    [Fact]
    public void OpenningHomePageWithManyBooks()
    {
        // Given I have a library
        string[] libraryNames = ["Warlock_Of_Firetop_Mountain", "Books_With_Pages"];
        var library = new XmlLibrary("../../../TestData", libraryNames);

        // when I open the home page
        BookEditorHomeViewModel homePage = new(library, new EditorStateCache());

        // then a book selector is displayed with 1 book
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        Assert.Equal(8, homePage.Books.Count());
        
    }
    
    [Fact]
    public void ReOpenningHomePageWithSelectedBookAndPage()
    {
        // Given I have a library
        string[] libraryNames = ["Warlock_of_Firetop_Mountain", "Books_With_Pages"];
        var library = new XmlLibrary("../../../TestData", libraryNames);
        var cache = new EditorStateCache();

        // when I open the home page
        BookEditorHomeViewModel homePage = new(library, cache);
        Assert.NotNull(homePage);
        
        // when I select the first book
        Assert.NotNull(homePage.Books);
        homePage.SelectBook(homePage.Books.First());
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Pages);

        // when I select the first page
        homePage.SelectPage(homePage.SelectedBook.Pages.First());

        // then the page selected details are displayed
        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal("Intro", homePage.SelectedPage.PageType);
        Assert.Equal(1, homePage.SelectedPage.Index);
        Assert.Equal(2, homePage.SelectedPage.Story.Blocks.Length);
        Assert.Equal(2, homePage.SelectedPage.Options.Length);

        // when I refresh the page
        homePage = new(library, cache);

        // then I the selected page details to be displayed
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Title = "The Warlock of Firetop Mountain");
        Assert.NotNull(homePage.SelectedPage);
        
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal("Intro", homePage.SelectedPage.PageType);
        Assert.Equal(1, homePage.SelectedPage.Index);
        Assert.Equal(2, homePage.SelectedPage.Story.Blocks.Length);
        Assert.Equal(2, homePage.SelectedPage.Options.Length);

    }

    [Fact]
    public void ReOpenningHomePageAfterChangingSelectedBookAndPage()
    {
        // Given I have a library
        string[] libraryNames = ["Warlock_of_Firetop_Mountain", "Books_With_Pages"];
        var library = new XmlLibrary("../../../TestData", libraryNames);
        var cache = new EditorStateCache();

        // when I open the home page
        BookEditorHomeViewModel homePage = new(library, cache);
        Assert.NotNull(homePage);
        
        // when I select the first book
        Assert.NotNull(homePage.Books);
        homePage.SelectBook(homePage.Books.First());
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Pages);

        // when I select the first page
        homePage.SelectPage(homePage.SelectedBook.Pages.First());

        // then the page selected details are displayed
        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal("Intro", homePage.SelectedPage.PageType);
        Assert.Equal(1, homePage.SelectedPage.Index);
        Assert.Equal(2, homePage.SelectedPage.Story.Blocks.Length);
        Assert.Equal(2, homePage.SelectedPage.Options.Length);
        
        // when I select the last book
        Assert.NotNull(homePage.Books);
        homePage.SelectBook(homePage.Books.Last());

        // when I refresh the page
        homePage = new(library, cache);


        // then I the selected page details to be displayed
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Title = "The Warlock of Firetop Mountain");
        Assert.Null(homePage.SelectedPage);
    }
}
namespace bookeditor.test;

public class BookEditorHomePage_LandingTests
{
    [Fact]
    public void FirstTimeOpenningHomePage()
    {
        // Given I have a library
        var libraryName = "Warlock_Of_Firetop_Mountain";
        var library = new XmlLibrary("../../../TestData", [libraryName]);
        var notificationQueue = new InMemoryNotificationsQueue();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new BookEditorHomeViewModel(library, notificationQueue);

        // when I open the home page

        // then a book selector is displayed with 1 book
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.LibraryBookSelector);
        Assert.Single(homePage.LibraryBookSelector.Books);

        // then an empty page list is displayed
        Assert.Null(homePage.SelectedBook);
        Assert.Null(homePage.SelectedBookPageList);
        Assert.NotNull(homePage.Books);
        Assert.Empty(homePage.Books);

        // then an empty page detail panel is displayed
        Assert.Null(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPageDetail);
    }


    [Fact]
    public void SelectABook()
    {
        // Given I have a library
        var libraryName = "Warlock_Of_Firetop_Mountain";
        var library = new XmlLibrary("../../../TestData", [libraryName]);
        var notificationQueue = new InMemoryNotificationsQueue();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new BookEditorHomeViewModel(library, notificationQueue);

        // when I open the home page

        // then a book selector is displayed with 1 book
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.LibraryBookSelector);
        Assert.Single(homePage.LibraryBookSelector.Books);

        // when I open the selected book
        homePage.OpenSelectedBook();

        // then the title of the book is displayed
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBookPageList);
        Assert.NotNull(homePage.SelectedBookPageList.Book);
        Assert.Equal("Warlock Of Firetop Mountain", homePage.SelectedBook.Title);
        Assert.Equal(homePage.SelectedBook.Title, homePage.SelectedBookPageList.Book.Title);
        
        // then 403 page list is displayed
        Assert.Equal(403, homePage.SelectedBook.Pages.Length);
        Assert.Equal(homePage.SelectedBook.Pages.Length, homePage.SelectedBookPageList.Book.Pages.Length);

        // then an empty page detail panel is displayed
        Assert.Null(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPageDetail);
    }
}
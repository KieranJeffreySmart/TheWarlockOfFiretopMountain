using bookeditor.ViewModels;

namespace bookeditor.test;

public class BookEditorHomePage_LandingTests
{
    [Fact]
    public async Task OpenningHomePageWithNoBooks()
    {
        // Given I have a library
        var libraryName = "Empty_Library";
        var library = new XmlLibrary("../../../TestData", [libraryName]);
        var notificationQueue = new InMemoryNotificationsQueue();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new(library, notificationQueue);

        // when I open the home page
        await homePage.PreRender();

        // then a book selector is displayed with no books
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        Assert.Empty(homePage.Books);
        
        // then I am informed that the library opened with 1 book
        Assert.True(notificationQueue.Any());
        Assert.Equal($"no books were found", notificationQueue.Pop());
    }

    [Fact]
    public async Task OpenningHomePageWithSingleBook()
    {
        // Given I have a library
        var libraryName = "Warlock_Of_Firetop_Mountain";
        var library = new XmlLibrary("../../../TestData", [libraryName]);
        var notificationQueue = new InMemoryNotificationsQueue();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new BookEditorHomeViewModel(library, notificationQueue);

        // when I open the home page
        await homePage.PreRender();

        // then a book selector is displayed with 1 book
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        Assert.Single(homePage.Books);
        Assert.Equal("Warlock of Firetop Mountain", homePage.Books.First().Title);

        // then I am informed that the library opened with 1 book
        Assert.True(notificationQueue.Any());
        Assert.Equal($"1 book was found", notificationQueue.Pop());
    }
    
    [Fact]
    public async Task OpenningHomePageWithManyBooks()
    {
        // Given I have a library
        string[] libraryNames = ["Warlock_Of_Firetop_Mountain", "Books_With_Pages"];
        var library = new XmlLibrary("../../../TestData", libraryNames);
        var notificationQueue = new InMemoryNotificationsQueue();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new(library, notificationQueue);

        // when I open the home page
        await homePage.PreRender();

        // then a book selector is displayed with 1 book
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        Assert.Equal(8, homePage.Books.Count());
        
        // then I am informed that the library opened with 1 book
        Assert.True(notificationQueue.Any());
        Assert.Equal($"8 books were found", notificationQueue.Pop());
    }
}
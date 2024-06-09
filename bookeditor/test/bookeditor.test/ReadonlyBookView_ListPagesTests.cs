using bookeditor.ViewModels;

namespace bookeditor.test;

public class ReadonlyBookView_ListPagesTests
{
    [Theory]
    [InlineData("Single Intro book", 1, new[] {"Intro Page 1"})]
    [InlineData("Single Game book", 1, new[] {"Game Page 1"})]
    [InlineData("Single Game and Intro book", 2, new[] {"Intro Page 1", "Game Page 1"})]
    [InlineData("Many Intro book", 3, new[] {"Intro Page 1", "Intro Page 2", "Intro Page 3"})]
    [InlineData("Many Game book", 3, new[] {"Game Page 1", "Game Page 2", "Game Page 3"})]
    [InlineData("Many Game and Intro book", 6, new[] {"Intro Page 1", "Intro Page 2", "Intro Page 3", "Game Page 1", "Game Page 2", "Game Page 3"})]
    public void ListSomePages(string testBook, int pageCount, string[] pageListItemLabels)
    {
        // Given I have a library
        var library = "Books_With_Pages";
        var testDataRepository = new XmlLibrary("../../../TestData", [library]);

        // Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has a book with a single intro page
        // Given I the library has a book with many intro pages
        // Given I the library has a book with a single game page
        // Given I the library has a book with many game pages
        // Given I the library has a book with a single intro page and game page
        // Given I the library has a book with many intro pages and game pages

        // When I open the book
        var viewModel = new BookPageListViewModel(testDataRepository, notificationQueue);
        viewModel.OpenBook(testBook);
        
        // Then there should be pages displayed
        Assert.True(viewModel.Pages.Any());
        Assert.Equal(pageCount, viewModel.Pages.Count);

        // Then each page should display the correct information
        Assert.Equal(pageListItemLabels, viewModel.Pages.Select(p => p.Label));

        // Then I am informed the book is open and the number of pages
        Assert.True(notificationQueue.Any());
        Assert.Equal($"The book [{testBook}] opened with {pageCount} page{(pageCount != 1 ? "s" : "")}", notificationQueue.Pop());
    }

    [Fact]
    public void ListManyPages()
    {
        // Given I have a library
        var library = "Warlock_of_Firetop_Mountain";
        var testDataRepository = new XmlLibrary("../../../TestData", [library]);

        // Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given the library has a book with a lot of pages
        string testBook = "Warlock of Firetop Mountain";
        int pageCount = 403;

        // When I open the book
        var viewModel = new BookPageListViewModel(testDataRepository, notificationQueue);
        viewModel.OpenBook(testBook);
        
        // Then the book title should be displayed
        Assert.Equal(testBook, viewModel.BookTitle);

        // Then there should be pages displayed
        Assert.True(viewModel.Pages.Any());
        Assert.Equal(pageCount, viewModel.Pages.Count);

        // Then I am informed the book is open and the number of pages
        Assert.True(notificationQueue.Any());
        Assert.Equal($"The book [Warlock of Firetop Mountain] opened with 403 pages", notificationQueue.Pop());
    }
    
    [Theory]
    [InlineData("Next and Back book")]
    // [InlineData("Single Goto book")]
    // [InlineData("Many Gotos book")]
    // [InlineData("Luck Test book")]
    // [InlineData("Skill Test book")]
    // [InlineData("Single Monster Fight book")]
    // [InlineData("Many Same Monster Fight book")]
    // [InlineData("Many Different Monster Fight book")]
    public void DisplaySomeOptions(string testBook)
    {
        // Given I have a library
        var library = "Books_With_Options";
        var testDataRepository = new XmlLibrary("../../../TestData", [library]);

        // Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given the library has a book with next and back

        // When I open the book
        var viewModel = new BookPageListViewModel(testDataRepository, notificationQueue);
        viewModel.OpenBook(testBook);

        // Then the number of options for each page should be displayed

        // Then I am informed the book is open and the number of pages
    }
}
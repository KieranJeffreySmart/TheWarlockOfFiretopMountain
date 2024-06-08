namespace bookeditor.test;

public class ReadonlyBookView_ViewModelTests
{
    [Fact]
    public void OpenMissingBook()
    {
        // Given I have a library
        var library = "Books_With_Pages";
        var testDataRepository = new XmlBookRepo("../../../TestData");

        //Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has an empty book
        var testBook = "Missing book";

        // when I open the book
        var viewModel = new BookPageListViewModel(testDataRepository, notificationQueue);
        viewModel.OpenBook(library, testBook);
        
        // Then I am informed the book is empty
        Assert.True(notificationQueue.Any());
        Assert.Equal($"The book was not found", notificationQueue.Pop());
        
        // Then there should be no pages displayed
        Assert.False(viewModel.Pages.Any());
    }

    [Fact]
    public void OpenEmptyBook()
    {
        // Given I have a library
        var library = "Books_With_Pages";
        var testDataRepository = new XmlBookRepo("../../../TestData");

        //Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has an empty book
        var testBook = "Empty book";

        // when I open the book
        var viewModel = new BookPageListViewModel(testDataRepository, notificationQueue);
        viewModel.OpenBook(library, testBook);
        
        // Then I am informed the book is empty
        Assert.True(notificationQueue.Any());
        Assert.Equal($"The book [{testBook}] opened but is empty", notificationQueue.Pop());
        
        // Then there should be no pages displayed
        Assert.False(viewModel.Pages.Any());
    }

    [Theory]
    [InlineData("Single Intro book", 1, new[] {"Intro Page 1"})]
    [InlineData("Single Game book", 1, new[] {"Game Page 1"})]
    [InlineData("Single Game and Intro book", 2, new[] {"Intro Page 1", "Game Page 1"})]
    [InlineData("Many Intro book", 3, new[] {"Intro Page 1", "Intro Page 2", "Intro Page 3"})]
    [InlineData("Many Game book", 3, new[] {"Game Page 1", "Game Page 2", "Game Page 3"})]
    [InlineData("Many Game and Intro book", 6, new[] {"Intro Page 1", "Intro Page 2", "Intro Page 3", "Game Page 1", "Game Page 2", "Game Page 3"})]
    public void OpenBookWithPages(string testBook, int pageCount, string[] pageListItemLabels)
    {
        // Given I have a library
        var library = "Books_With_Pages";
        var testDataRepository = new XmlBookRepo("../../../TestData");

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
        viewModel.OpenBook(library, testBook);
        
        // Then there should be pages displayed
        Assert.True(viewModel.Pages.Any());
        Assert.Equal(pageCount, viewModel.Pages.Count);

        // Then each page should display the correct information
        Assert.Equal(pageListItemLabels, viewModel.Pages.Select(p => p.Label));

        // Then I am informed the book is open and the number of pages
        Assert.True(notificationQueue.Any());
        Assert.Equal($"The book [{testBook}] opened with {pageCount} page{(pageCount != 1 ? "s" : "")}", notificationQueue.Pop());
    }
}
namespace bookeditor.test;

public class ReadonlyBookView_ViewModelTests
{
    [Fact]
    public void OpenEmptyBook()
    {
        // Given I have a library
        var library = "Books_With_Pages";
        var testDataRepository = new XmlBookRepo("../../../TestData");

        // Given I the library has an empty book
        var testBook = "Empty Book";

        // when I open the book
        var viewModel = new BookPageListViewModel(testDataRepository);
        viewModel.OpenBook(library, testBook);
        
        // Then I am informed the book is empty
        Assert.True(viewModel.Notifications.Any());
        Assert.Equal("The book opened but is empty", viewModel.Notifications.First().Description);
        
        // Then there should be no pages displayed
        Assert.False(viewModel.Pages.Any());
    }

    [Theory]
    [InlineData("Single Intro Book", 1, new[] {"Intro Page 1"})]
    [InlineData("Single Game Page Book", 1, new[] {"Game Page 1"})]
    [InlineData("Many Intro Book", 3, new[] {"Intro Page 1", "Intro Page 2", "Intro Page 3"})]
    [InlineData("Many Game Page Book", 3, new[] {"Game Page 1", "Game Page 2", "Game Page 3"})]
    [InlineData("Single Game and Intro Page Book", 2, new[] {"1", "Game Page 1"})]
    [InlineData("Many Game and Intro Page Book", 6, new[] {"Intro Page 1", "Intro Page 2", "Intro Page 3", "Game Page 1", "Game Page 2", "Game Page 3"})]
    public void OpenBookWithPages(string testBook, int pageCount, string[] pageListItemLabels)
    {
        // Given I have a library
        var library = "Books_With_Pages";
        var testDataRepository = new XmlBookRepo("../../../TestData");

        // Given I the library has a book with a single intro page
        // Given I the library has a book with many intro pages
        // Given I the library has a book with a single game page
        // Given I the library has a book with many game pages
        // Given I the library has a book with a single intro page and game page
        // Given I the library has a book with many intro pages and game pages

        // When I open the book
        var viewModel = new BookPageListViewModel(testDataRepository);
        viewModel.OpenBook(library, testBook);
        
        // Then there should be pages displayed
        Assert.True(viewModel.Pages.Any());
        Assert.True(viewModel.Pages.Count == pageCount);

        // Then each page should display the correct information
        Assert.Equal(pageListItemLabels, viewModel.Pages.Select(p => p.Label));

        // Then I am informed the book is open and the number of pages
        Assert.True(viewModel.Notifications.Any());
        Assert.True(viewModel.Notifications.First().Description == $"The book opened with {pageCount} pages");
    }
}
using bookeditor.ViewModels;

namespace bookeditor.test;

public class ReadOnlyBookView_OpenBookTests
{
    [Fact]
    public void OpenMissingBook()
    {
        // Given I have a library
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        //Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has an empty book
        var testBook = "Missing book";

        // when I open the book
        var viewModel = new BookPageListViewModel() { Book = library.GetBook(testBook) };
        
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
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        //Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has an empty book
        var testBook = "Empty book";

        // when I open the book
        var viewModel = new BookPageListViewModel() { Book = library.GetBook(testBook) };
        
        // Then I am informed the book is empty
        Assert.True(notificationQueue.Any());
        Assert.Equal($"The book [{testBook}] opened but is empty", notificationQueue.Pop());
    }
    
    [Fact]
    public void OpenSelectedBook()
    {
        // Given I have a library
        var library = "Books_With_Pages";
        var testDataRepository = new XmlLibrary("../../../TestData", [library]);

        //Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has an empty book
        var testBook = "Empty book";

        var viewModel = new BookPageListViewModel();
        
        // when I select a book
        viewModel.Book = testDataRepository.GetBook(testBook);
        
        // Then I am informed the book is empty
        Assert.True(notificationQueue.Any());
        Assert.Equal($"The book [{testBook}] opened but is empty", notificationQueue.Pop());
        
        // Then there should be no pages displayed
        Assert.False(viewModel.Pages.Any());
    }

    [Fact]
    public void OpenBookFromNewPath()
    {
        // given I have a library on file in a new folder
        var libraryName = "Warlock_of_Firetop_Mountain";
        var library = new XmlLibrary("", [libraryName]);

        // given I have a book title
        var booktitle = "Warlock of Firetop Mountain";

        // Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // when I get the book
        var viewModel = new BookPageListViewModel() { Book = library.GetBook(booktitle) };

        // Then I am informed the book is missing
        Assert.True(notificationQueue.Any());
        Assert.Equal($"The book was not found", notificationQueue.Pop());
        
        // Then there should be no pages displayed
        Assert.False(viewModel.Pages.Any());

        // given I have a new root path
        string rootPath = "../../../TestData";
        
        // when I set the root path
        library.RootPath = rootPath;

        // when I get the book
        viewModel.Book = library.GetBook(booktitle);

        // Then the book title should be displayed
        Assert.Equal(booktitle, viewModel.Book.Title);

        // Then there should be pages displayed
        Assert.True(viewModel.Pages.Any());
        Assert.Equal(403, viewModel.Pages.Count());

        // Then I am informed the book is open and the number of pages
        Assert.True(notificationQueue.Any());
        Assert.Equal($"The book [Warlock of Firetop Mountain] opened with 403 pages", notificationQueue.Pop());
    }
}
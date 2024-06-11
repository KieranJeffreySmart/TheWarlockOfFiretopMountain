using bookeditor.ViewModels;

namespace bookeditor.test;

public class ReadOnlyBookView_OpenBookTests
{
    [Fact]
    public async Task OpenMissingBook()
    {
        // Given I have a library
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        //Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has an empty book
        var testBook = "Missing book";

        // when I open the book
        var viewModel = new BookEditorHomeViewModel(library, notificationQueue);
        Assert.NotNull(viewModel);
        await viewModel.PreRender();
        Assert.NotNull(viewModel.Books);
        
        // Then I am informed the book is empty
        Assert.True(notificationQueue.Any());
        Assert.Equal($"7 books were found", notificationQueue.Pop());
        
        // Then there should be no pages displayed
        Assert.Null(viewModel.SelectedBook);
    }

    [Fact]
    public async Task OpenEmptyBook()
    {
        // Given I have a library
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        //Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has an empty book
        var testBook = "Empty book";

        // when I open the book
        var viewModel = new BookEditorHomeViewModel(library, notificationQueue);
        Assert.NotNull(viewModel);
        await viewModel.PreRender();
        Assert.NotNull(viewModel.Books);
        
        // Then I am informed the book is empty
        Assert.True(notificationQueue.Any());
        Assert.Equal($"7 books were found", notificationQueue.Pop());
    }
    
    [Fact]
    public async Task OpenSelectedBook()
    {
        // Given I have a library
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        //Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has an empty book
        var testBook = "Empty book";

        var viewModel = new BookEditorHomeViewModel(library, notificationQueue);
        Assert.NotNull(viewModel);
        await viewModel.PreRender();
        Assert.NotNull(viewModel.Books);
        
        // when I select a book
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        
        // Then I am informed the book is empty
        Assert.True(notificationQueue.Any());
        Assert.Equal($"7 books were found", notificationQueue.Pop());
        
        // Then there should be no pages displayed
        Assert.Null(viewModel.SelectedBook.Pages);
    }
}
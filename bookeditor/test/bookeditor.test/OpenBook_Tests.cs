using bookeditor.ViewModels;

namespace bookeditor.test;

public class OpenBook_Tests
{
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
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedViewModels();


        // then the title is displayed
        Assert.NotNull(viewModel.SelectedBookDetails);
        Assert.NotNull(viewModel.SelectedBookDetails.Book);
        Assert.Equal(testBook, viewModel.SelectedBookDetails.Book.Title);
        // then a page count with 0 is displyed
        Assert.Equal(0, viewModel.SelectedBookDetails.PageCount);
    }
    
    [Fact]
    public async Task OpenBookWithPages()
    {
        // Given I have a library
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        //Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has an empty book
        var testBook = "Single Intro book";

        var viewModel = new BookEditorHomeViewModel(library, notificationQueue);
        Assert.NotNull(viewModel);
        await viewModel.PreRender();
        Assert.NotNull(viewModel.Books);
        
        // when I select a book
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedViewModels();
        
        // then the title is displayed
        Assert.NotNull(viewModel.SelectedBookDetails);
        Assert.NotNull(viewModel.SelectedBookDetails.Book);
        Assert.Equal(testBook, viewModel.SelectedBookDetails.Book.Title);
        // then a page count with 0 is displyed
        Assert.Equal(1, viewModel.SelectedBookDetails.PageCount);
    }
    
    [Fact]
    public async Task OpenBookCompleteBook()
    {
        // Given I have a library
        var libraryName = "Warlock_of_Firetop_Mountain";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        //Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // Given I the library has an empty book
        var testBook = "Warlock of Firetop Mountain";

        var viewModel = new BookEditorHomeViewModel(library, notificationQueue);
        Assert.NotNull(viewModel);
        await viewModel.PreRender();
        Assert.NotNull(viewModel.Books);
        
        // when I select a book
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedViewModels();
        
        // then the title is displayed
        Assert.NotNull(viewModel.SelectedBookDetails);
        Assert.NotNull(viewModel.SelectedBookDetails.Book);
        Assert.Equal(testBook, viewModel.SelectedBookDetails.Book.Title);
        // then a page count with 403 is displyed
        Assert.Equal(403, viewModel.SelectedBookDetails.PageCount);
    }
}
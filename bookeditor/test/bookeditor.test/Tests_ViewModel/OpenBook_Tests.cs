using bookeditor.ViewModels;

namespace bookeditor.test;

public class OpenBook_Tests
{
    [Fact]
    public void OpenEmptyBook()
    {
        // Given I have a library
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);


        // Given I the library has an empty book
        var testBook = "Empty book";

        // when I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Books);
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedBook();


        // then the title is displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.Equal(testBook, viewModel.SelectedBook?.Title);
        // then a page count with 0 is displyed
        Assert.Null(viewModel.SelectedBook?.Pages);
    }
    
    [Fact]
    public void OpenBookWithPages()
    {
        // Given I have a library
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // Given I the library has an empty book
        var testBook = "Single Intro book";

        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Books);
        
        // when I select a book
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedBook();
        
        // then the title is displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.Equal(testBook, viewModel.SelectedBook?.Title);
        // then a page count with 0 is displyed
        Assert.Equal(1, viewModel.SelectedBook?.Pages.Length);
    }
    
    [Fact]
    public void OpenBookCompleteBook()
    {
        // Given I have a library
        var libraryName = "Warlock_of_Firetop_Mountain";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // Given I the library has an empty book
        var testBook = "Warlock of Firetop Mountain";

        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Books);
        
        // when I select a book
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedBook();
        
        // then the title is displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.Equal(testBook, viewModel.SelectedBook?.Title);
        // then a page count with 0 is displyed
        Assert.Equal(403, viewModel.SelectedBook?.Pages.Length);
    }
}
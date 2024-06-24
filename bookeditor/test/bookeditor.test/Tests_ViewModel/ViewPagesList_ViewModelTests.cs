using bookeditor.ViewModels;

namespace bookeditor.test;

using Arrange = bookeditor.test.ViewModelArrangement;

public class ViewPagesList_ViewModelTests
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
        var libraryName = "Books_With_Pages";
        var viewModel = Arrange.CreateHomePageVM("../../../TestData", [libraryName]);

        // When I open the book
        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Books);
        Assert.NotEmpty(viewModel.Books);
        viewModel.SelectBook(viewModel.Books.First(b => b.Title == testBook));

        // then the title is displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.Equal(testBook, viewModel.SelectedBook.Title);

        // then a page count is displyed
        Assert.Equal(pageCount, viewModel.SelectedBook.Pages.Length);
    }

    [Fact]
    public void ListManyPages()
    {
        // Given I have a library
        var libraryName = "Warlock_of_Firetop_Mountain";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // Given the library has a book with a lot of pages
        string testBook = "Warlock of Firetop Mountain";
        int pageCount = 403;

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Books);
        viewModel.SelectBook(viewModel.Books.First(b => b.Title == testBook));        
        
        // then the title is displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.Equal(testBook, viewModel.SelectedBook.Title);

        // then a page count is displyed
        Assert.Equal(pageCount, viewModel.SelectedBook.Pages.Length);
    }
}
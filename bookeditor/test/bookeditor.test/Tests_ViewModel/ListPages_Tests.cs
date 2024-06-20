using bookeditor.ViewModels;

namespace bookeditor.test;

public class ListPages_ViewModelTests
{
    [Theory]
    [InlineData("Single Intro book", 1, new[] {"Intro Page 1"})]
    [InlineData("Single Game book", 1, new[] {"Game Page 1"})]
    [InlineData("Single Game and Intro book", 2, new[] {"Intro Page 1", "Game Page 1"})]
    [InlineData("Many Intro book", 3, new[] {"Intro Page 1", "Intro Page 2", "Intro Page 3"})]
    [InlineData("Many Game book", 3, new[] {"Game Page 1", "Game Page 2", "Game Page 3"})]
    [InlineData("Many Game and Intro book", 6, new[] {"Intro Page 1", "Intro Page 2", "Intro Page 3", "Game Page 1", "Game Page 2", "Game Page 3"})]
    public async Task ListSomePages(string testBook, int pageCount, string[] pageListItemLabels)
    {
        // Given I have a library
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // Given I the library has a book with a single intro page
        // Given I the library has a book with many intro pages
        // Given I the library has a book with a single game page
        // Given I the library has a book with many game pages
        // Given I the library has a book with a single intro page and game page
        // Given I the library has a book with many intro pages and game pages

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        await viewModel.Init();
        Assert.NotNull(viewModel.Books);
        Assert.NotEmpty(viewModel.Books);
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedBook();
        
        // then the title is displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.Equal(testBook, viewModel.SelectedBook.Title);

        // then a page count is displyed
        Assert.Equal(pageCount, viewModel.SelectedBook.Pages.Length);
    }

    [Fact]
    public async Task ListManyPages()
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
        await viewModel.Init();
        Assert.NotNull(viewModel.Books);
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedBook();
        
        // then the title is displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.Equal(testBook, viewModel.SelectedBook.Title);

        // then a page count is displyed
        Assert.Equal(pageCount, viewModel.SelectedBook.Pages.Length);
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
    public async Task DisplaySomeOptions(string testBook)
    {
        // Given I have a library
        var libraryName = "Books_With_Options";
        var library = new XmlLibrary("../../../TestData", [libraryName]);


        // Given the library has a book with next and back

        // When I open the book        
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        await viewModel.Init();
        Assert.NotNull(viewModel.Books);

        // Then the number of options for each page should be displayed

        // Then I am informed the book is open and the number of pages
    }
}
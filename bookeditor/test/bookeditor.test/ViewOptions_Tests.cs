using bookeditor.ViewModels;

namespace bookeditor.test;

public class ViewOptions_Tests 
{

    [Theory]
    [InlineData("Start Command", 0, 1, new[] {"s"}, new[] {"Start"}, new[] {"START_GAME"})]
    [InlineData("Quit Command", 0, 1, new[] {"q"}, new[] {"Quit"}, new[] {"QUIT_GAME"})]
    [InlineData("Continue Command", 0, 1, new[] {"c"}, new[] {"Continue"}, new[] {"NEXT_PAGE"})]
    [InlineData("Continue and Back Commands", 0, 1, new[] {"c"}, new[] {"Continue"}, new[] {"NEXT_PAGE"})]
    [InlineData("Continue and Back Commands", 1, 1, new[] {"b"}, new[] {"Back"}, new[] {"PREVIOUS_PAGE"})]
    [InlineData("Goto Command", 0, 1, new[] {"g"}, new[] {"Go to Page 3"}, new[] {"GOTO_PAGE"})]
    [InlineData("Goto Command", 1, 2, new[] {"g", "g"}, new[] {"Go to Page 1", "Go to Page 3"}, new[] {"GOTO_PAGE", "GOTO_PAGE"})]
    [InlineData("Goto Continue and Back", 1, 3, new[] {"g", "c", "b"}, new[] {"Go to Page 1", "Continue", "Back"}, new[] {"GOTO_PAGE", "NEXT_PAGE", "PREVIOUS_PAGE"})]
    public async Task ViewPageOptions(string testBook, int pageArrayIndex, int optionCount, string[] optionsKeys, string[] labels, string[] commands)
    {
        // Given I have a library
        var libraryName = "Books_With_Options";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, notificationQueue);
        Assert.NotNull(viewModel);
        await viewModel.PreRender();
        Assert.NotNull(viewModel.Books);
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedViewModels();
        
        // Then there should be pages displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.NotNull(viewModel.SelectedBook.Pages);
        Assert.NotEmpty(viewModel.SelectedBook.Pages);

        // When the page is selected
        viewModel.SelectedPage = viewModel.SelectedBook.Pages[pageArrayIndex];
        viewModel.UpdateSelectedViewModels();

        // then the options are displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPageDetails);
        Assert.NotNull(viewModel.SelectedPageDetails.Page);

        Assert.Equal(optionCount, viewModel.SelectedPageDetails.Page.Options.Length);
        Assert.Equal(optionsKeys, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Key).ToArray());
        Assert.Equal(labels, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Label).ToArray());
        Assert.Equal(commands, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Command).ToArray());
    }
}
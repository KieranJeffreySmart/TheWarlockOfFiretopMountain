using bookeditor.ViewModels;

namespace bookeditor.test;

public class ViewOptions_Tests 
{

    [Theory]
    [InlineData("Single option", 0, 1, new[] {"n"}, new[] {"Next"}, new[] {"NEXT_PAGE"})]
    public async Task ViewPageOptions(string testBook, int pageNumber, int optionCount, string[] optionsKeys, string[] labels, string[] commands)
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
        viewModel.SelectedPage = viewModel.SelectedBook.Pages[pageNumber];
        viewModel.UpdateSelectedViewModels();

        // then the options are displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPageDetails);
        Assert.NotNull(viewModel.SelectedPageDetails.Page);

        // Assert.Equal(optionCount, viewModel.SelectedPageDetails.Page.Options.Length);
        // Assert.Equal(optionsKeys, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Key).ToArray());
        // Assert.Equal(labels, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Label).ToArray());
        // Assert.Equal(commands, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Command).ToArray());
    }
}
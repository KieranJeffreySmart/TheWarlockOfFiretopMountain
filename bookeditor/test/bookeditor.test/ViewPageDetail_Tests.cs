using bookeditor.ViewModels;

namespace bookeditor.test;

public class ViewPageDetail_Tests
{
    [Theory]
    [InlineData("Single caret story", 0, 1, "My single caret story")]
    [InlineData("Some caret story", 0, 1, "My first caretMy second caret\n                \nMy third caret\n                ")]
    public async Task ViewFirstPage(string testBook, int pageNumber, int pageIndex, string story)
    {
        // Given I have a library
        var libraryName = "Books_With_Stories";
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


        // then the story text is displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPageDetails);
        Assert.NotNull(viewModel.SelectedPageDetails.Page);
        Assert.Equal(pageIndex, viewModel.SelectedPageDetails.Page.Index);
        Assert.Equal("Intro", viewModel.SelectedPageDetails.Page.PageType);
        Assert.Equal(story, viewModel.SelectedPageDetails.StoryTextRaw);



        //[rgR] enable this test when it is possible to tet the UI
        // Assert.Equal(story, viewModel.SelectedPage.StoryTextRaw);
    }
}
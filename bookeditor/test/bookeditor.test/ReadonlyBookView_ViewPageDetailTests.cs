using bookeditor.ViewModels;

namespace bookeditor.test;

public class ReadonlyBookView_ViewPageDetailTests
{
    [Theory]
    [InlineData("Single caret story", "My single caret story")]
    [InlineData("Some caret story", "My first caretMy second caret\n                \nMy third caret\n                ")]
    public async Task ViewFirstPage(string testBook, string story)
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
        
        // Then there should be pages displayed
        Assert.Null(viewModel.SelectedBook);

        // Then the first page is selected
        Assert.NotNull(viewModel.SelectedPage);
        Assert.Equal(1, viewModel.SelectedPage.Index);
        Assert.Equal("Intro", viewModel.SelectedPage.PageType);

        // then the story text is displayed
        //[rgR] enable this test when it is possible to tet the UI
        // Assert.Equal(story, viewModel.SelectedPage.StoryTextRaw);
    }
}
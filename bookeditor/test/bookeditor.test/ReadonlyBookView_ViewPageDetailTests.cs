using bookeditor.ViewModels;

namespace bookeditor.test;

public class ReadonlyBookView_ViewPageDetailTests
{
    [Theory]
    [InlineData("Single caret story", "My single caret story")]
    [InlineData("Some caret story", "My first caretMy second caret\n                \nMy third caret")]
    public void ViewFirstPage(string testBook, string story)
    {
        // Given I have a library
        var library = "Books_With_Stories";
        var testDataRepository = new XmlLibrary("../../../TestData", [library]);

        // Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // When I open the book
        var viewModel = new BookPageListViewModel(testDataRepository, notificationQueue);
        viewModel.OpenBook(testBook);
        
        // Then there should be pages displayed
        Assert.True(viewModel.Pages.Any());

        // Then the first page is selected
        Assert.NotNull(viewModel.SelectedPage);
        Assert.Equal(1, viewModel.SelectedPage.Index);

        // then the story text is displayed
        Assert.Equal(story, viewModel.SelectedPage.StoryTextRaw.Trim());
    }
}
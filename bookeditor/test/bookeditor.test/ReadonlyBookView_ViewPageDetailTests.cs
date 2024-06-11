using bookeditor.ViewModels;

namespace bookeditor.test;

public class ReadonlyBookView_ViewPageDetailTests
{
    [Theory]
    [InlineData("Single caret story", "My single caret story")]
    [InlineData("Some caret story", "My first caretMy second caret\n                \nMy third caret\n                ")]
    public void ViewFirstPage(string testBook, string story)
    {
        // Given I have a library
        var libraryName = "Books_With_Stories";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // Given I have a notification service
        InMemoryNotificationsQueue notificationQueue = new InMemoryNotificationsQueue();

        // When I open the book
        var listViewModel = new BookPageListViewModel() { Book = library.GetBook(testBook) };
        
        // Then there should be pages displayed
        Assert.True(listViewModel.Pages.Any());

        // Then the first page is selected
        Assert.NotNull(listViewModel.SelectedPage);
        Assert.Equal(1, listViewModel.SelectedPage.Index);
        Assert.Equal("Intro", listViewModel.SelectedPage.Type);

        // then the story text is displayed
        PageDetailViewModel detailViewModel = new PageDetailViewModel();
        detailViewModel.Page = listViewModel.SelectedPage;
        Assert.Equal(story, detailViewModel.StoryTextRaw);
    }
}
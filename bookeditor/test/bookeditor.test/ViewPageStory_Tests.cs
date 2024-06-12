using bookeditor.ViewModels;

namespace bookeditor.test;

public class ViewPageStory_Tests
{
    [Theory]
    [InlineData("Single caret story", new string[] {"My single caret"}, "My single caret", new string[] {})]
    [InlineData(
        "Many caret story",
        new string[] {"My first caret", "My second caret\n                ", "\nMy third caret\n                "}, 
        "My first caretMy second caret\n                \nMy third caret\n                ", 
        new string[] {})]
    [InlineData("Single image story", new string[] {}, "", new string[] {"image1.jpg"})]
    [InlineData("Many image story", new string[] {}, "", new string[] {"image1.jpg", "image2.png", "image3.bmp"})]
    [InlineData("Single image and caret story", new string[] {"My single caret"}, "My single caret", new string[] {"image1.jpg"})]
    [InlineData("Many image and caret story",
        new string[] {"My first caret", "My second caret\n                ", "\nMy third caret\n                "}, 
        "My first caretMy second caret\n                \nMy third caret\n                ", 
        new string[] {"image1.jpg", "image2.png", "image3.bmp"})]
    public async Task ViewPageStory(string testBook, string[] carets, string rawText, string[] images)
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
        viewModel.SelectedPage = viewModel.SelectedBook.Pages[0];
        viewModel.UpdateSelectedViewModels();

        // then the story text is displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPageDetails);
        Assert.NotNull(viewModel.SelectedPageDetails.Page);
        Assert.NotNull(viewModel.SelectedPageDetails.Page.Story);
        Assert.NotNull(viewModel.SelectedPageDetails.Page.Story.TextCarets);

        Assert.Equal(carets, viewModel.SelectedPageDetails.Page.Story.TextCarets);
        Assert.Equal(rawText, viewModel.SelectedPageDetails.StoryTextRaw);
        Assert.Equal(images, viewModel.SelectedPageDetails.Page.Story.Images);
    }


    [Theory]
    [InlineData("Single caret scene", new string[] {"My single caret"}, "My single caret", new string[] {})]
    [InlineData(
        "Many caret scene",
        new string[] {"My first caret", "My second caret\n                ", "\nMy third caret\n                "}, 
        "My first caretMy second caret\n                \nMy third caret\n                ", 
        new string[] {})]
    [InlineData("Single image scene", new string[] {}, "", new string[] {"image1.jpg"})]
    [InlineData("Many image scene", new string[] {}, "", new string[] {"image1.jpg", "image2.png", "image3.bmp"})]
    [InlineData("Single image and caret scene", new string[] {"My single caret"}, "My single caret", new string[] {"image1.jpg"})]
    [InlineData("Many image and caret scene",
        new string[] {"My first caret", "My second caret\n                ", "\nMy third caret\n                "}, 
        "My first caretMy second caret\n                \nMy third caret\n                ", 
        new string[] {"image1.jpg", "image2.png", "image3.bmp"})]
    public async Task ViewPageScene(string testBook, string[] carets, string rawText, string[] images)
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
        viewModel.SelectedPage = viewModel.SelectedBook.Pages[0];
        viewModel.UpdateSelectedViewModels();

        // then the scene text is displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPageDetails);
        Assert.NotNull(viewModel.SelectedPageDetails.Page);
        Assert.NotNull(viewModel.SelectedPageDetails.Page.Scene);
        Assert.NotNull(viewModel.SelectedPageDetails.Page.Scene.TextCarets);

        Assert.Equal(carets, viewModel.SelectedPageDetails.Page.Scene.TextCarets);
        Assert.Equal(rawText, viewModel.SelectedPageDetails.SceneTextRaw);
        Assert.Equal(images, viewModel.SelectedPageDetails.Page.Scene.Images);
    }
}
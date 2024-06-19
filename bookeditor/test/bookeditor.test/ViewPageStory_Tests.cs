using bookeditor.ViewModels;

namespace bookeditor.test;

public class ViewPageStory_Tests
{
    [Theory]
    [InlineData("Single caret story", new string[] {"My single caret"}, "My single caret")]
    [InlineData(
        "Many caret story",
        new string[] {"My first caret", "My second caret\n                ", "\nMy third caret\n                "}, 
        "My first caretMy second caret\n                \nMy third caret\n                ")]
    [InlineData("Single image story", new string[] {"image1.jpg"}, "")]
    [InlineData("Many image story", new string[] {"image1.jpg", "image2.png", "image3.bmp"}, "")]
    [InlineData("Single image and caret story", new string[] {"My single caret", "image1.jpg"}, "My single caret")]
    [InlineData("Many image and caret story",
        new string[] {"My first caret", "My second caret\n                ", "\nMy third caret\n                ", "image1.jpg", "image2.png", "image3.bmp"}, 
        "My first caretMy second caret\n                \nMy third caret\n                ")]
    public async Task ViewPageStory(string testBook, string[] carets, string rawText)
    {
        // Given I have a library
        var libraryName = "Books_With_Stories";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        await viewModel.Init();
        Assert.NotNull(viewModel.Books);
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedBook();
        
        // Then there should be pages displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.NotNull(viewModel.SelectedBook.Pages);
        Assert.NotEmpty(viewModel.SelectedBook.Pages);

        // When the page is selected
        viewModel.SelectedPage = viewModel.SelectedBook.Pages[0];
        viewModel.UpdateSelectedPage();

        // then the story text is displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPage.Story);

        if (carets == null)
        {
            Assert.Null(viewModel.SelectedPage.Story.Carets);
        }
        else
        {
            Assert.NotNull(viewModel.SelectedPage.Story.Carets);
            Assert.Equal(carets, viewModel.SelectedPage.Story.Carets.Select(c => c.StringValue));
        }

        Assert.NotNull(viewModel.SelectedPagePreview.Page);
        Assert.Equal(rawText, viewModel.SelectedPagePreview.StoryTextRaw);
    }


    [Theory]
    [InlineData("Single caret scene", new string[] {"My single caret"}, "My single caret")]
    [InlineData(
        "Many caret scene",
        new string[] {"My first caret", "My second caret\n                ", "\nMy third caret\n                "}, 
        "My first caretMy second caret\n                \nMy third caret\n                ")]
    [InlineData("Single image scene",  new string[] {"image1.jpg"}, "")]
    [InlineData("Many image scene", new string[] {"image1.jpg", "image2.png", "image3.bmp"}, "")]
    [InlineData("Single image and caret scene", new string[] {"My single caret", "image1.jpg"}, "My single caret")]
    [InlineData("Many image and caret scene",
        new string[] {"My first caret", "My second caret\n                ", "\nMy third caret\n                ", "image1.jpg", "image2.png", "image3.bmp"}, 
        "My first caretMy second caret\n                \nMy third caret\n                ")]
    public async Task ViewPageScene(string testBook, string[] carets, string rawText)
    {
        // Given I have a library
        var libraryName = "Books_With_Scenes";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        await viewModel.Init();
        Assert.NotNull(viewModel.Books);
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == testBook);
        viewModel.UpdateSelectedBook();
        
        // Then there should be pages displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.NotNull(viewModel.SelectedBook.Pages);
        Assert.NotEmpty(viewModel.SelectedBook.Pages);

        // When the page is selected
        viewModel.SelectedPage = viewModel.SelectedBook.Pages[0];
        viewModel.UpdateSelectedPage();

        // then the scene text is displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPage.Scene);

        if (carets == null)
        {
            Assert.Null(viewModel.SelectedPage.Scene.Carets);
        }
        else
        {
            Assert.NotNull(viewModel.SelectedPage.Scene.Carets);
            Assert.Equal(carets, viewModel.SelectedPage.Scene.Carets.Select(c => c.StringValue));
        }
        Assert.NotNull(viewModel.SelectedPagePreview.Page);
        Assert.Equal(rawText, viewModel.SelectedPagePreview.SceneTextRaw);
    }
}
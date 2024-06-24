using bookeditor.ViewModels;

namespace bookeditor.test;

public class ViewPageStory_Tests
{
    [Theory]
    [InlineData("Single block story", new string[] {"My single block"}, "My single block")]
    [InlineData(
        "Many block story",
        new string[] {"My first block", "My second block\n                ", "\nMy third block\n                "}, 
        "My first blockMy second block\n                \nMy third block\n                ")]
    [InlineData("Single image story", new string[] {"image1.jpg"}, "")]
    [InlineData("Many image story", new string[] {"image1.jpg", "image2.png", "image3.bmp"}, "")]
    [InlineData("Single image and block story", new string[] {"My single block", "image1.jpg"}, "My single block")]
    [InlineData("Many image and block story",
        new string[] {"My first block", "My second block\n                ", "\nMy third block\n                ", "image1.jpg", "image2.png", "image3.bmp"}, 
        "My first blockMy second block\n                \nMy third block\n                ")]
    public void ViewPageStory(string testBook, string[] blocks, string rawText)
    {
        // Given I have a library
        var libraryName = "Books_With_Stories";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Books);
        viewModel.SelectBook(viewModel.Books.First(b => b.Title == testBook));
        
        // Then there should be pages displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.NotNull(viewModel.SelectedBook.Pages);
        Assert.NotEmpty(viewModel.SelectedBook.Pages);

        // When the page is selected
        viewModel.SelectPage(viewModel.SelectedBook.Pages[0]);
        

        // then the story text is displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPage.Story);

        if (blocks == null)
        {
            Assert.Null(viewModel.SelectedPage.Story.Blocks);
        }
        else
        {
            Assert.NotNull(viewModel.SelectedPage.Story.Blocks);
            Assert.Equal(blocks, viewModel.SelectedPage.Story.Blocks.Select(c => c.StringValue));
        }

        // Assert.NotNull(viewModel.SelectedPagePreview.Page);
        // Assert.Equal(rawText, viewModel.SelectedPagePreview.StoryTextRaw);
    }


    [Theory]
    [InlineData("Single block scene", new string[] {"My single block"}, "My single block")]
    [InlineData(
        "Many block scene",
        new string[] {"My first block", "My second block\n                ", "\nMy third block\n                "}, 
        "My first blockMy second block\n                \nMy third block\n                ")]
    [InlineData("Single image scene",  new string[] {"image1.jpg"}, "")]
    [InlineData("Many image scene", new string[] {"image1.jpg", "image2.png", "image3.bmp"}, "")]
    [InlineData("Single image and block scene", new string[] {"My single block", "image1.jpg"}, "My single block")]
    [InlineData("Many image and block scene",
        new string[] {"My first block", "My second block\n                ", "\nMy third block\n                ", "image1.jpg", "image2.png", "image3.bmp"}, 
        "My first blockMy second block\n                \nMy third block\n                ")]
    public void ViewPageScene(string testBook, string[] blocks, string rawText)
    {
        // Given I have a library
        var libraryName = "Books_With_Scenes";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.Books);
        viewModel.SelectBook(viewModel.Books.First(b => b.Title == testBook));
        
        // Then there should be pages displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.NotNull(viewModel.SelectedBook.Pages);
        Assert.NotEmpty(viewModel.SelectedBook.Pages);

        // When the page is selected
        viewModel.SelectPage(viewModel.SelectedBook.Pages[0]);
        

        // then the scene text is displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPage.Scene);

        if (blocks == null)
        {
            Assert.Null(viewModel.SelectedPage.Scene.Blocks);
        }
        else
        {
            Assert.NotNull(viewModel.SelectedPage.Scene.Blocks);
            Assert.Equal(blocks, viewModel.SelectedPage.Scene.Blocks.Select(c => c.StringValue));
        }
        // Assert.NotNull(viewModel.SelectedPagePreview.Page);
        // Assert.Equal(rawText, viewModel.SelectedPagePreview.SceneTextRaw);
    }
}
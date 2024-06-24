using bookeditor.ViewModels;

namespace bookeditor.test;

using Arrange = bookeditor.test.ViewModelArrangement;
public class PageStoryUpdate_Tests
{    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/ChangeStoryBlockdBook.xml", "../../../TestData/Books_With_Stories.xml")]
    public void ChangeStoryBlockdBook()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "ChangeStoryBlockdBook";
        var homePage = Arrange.CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        Assert.NotNull(homePage);
        
        // given I have opened a book with a story text bloxk
        var bookSlug = "596d5e6a-3cfb-41af-b855-8bafa5a632f3";
        Arrange.OpenPage(homePage, bookSlug);
        var page = homePage.SelectedPage;

        Assert.NotNull(page);
        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Blocks);
        Assert.NotEmpty(page.Story.Blocks);

        var block = page.Story.Blocks.First();
        Assert.Equal("My single block", block.StringValue);

        // when I change the story
        block.StringValue = "new test value";
        homePage.ApplyChanges();

        // when I save to file
        homePage.SaveToFile();

        // when I re-open the app
        homePage = Arrange.CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        Assert.NotNull(homePage);
        Arrange.OpenPage(homePage, bookSlug);
        page = homePage.SelectedPage;
        Assert.NotNull(page);
        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Blocks);
        Assert.NotEmpty(page.Story.Blocks);
        block = page.Story.Blocks.First();
        Assert.Equal("new test value", block.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/ChangeSceneBlockdBook.xml", "../../../TestData/Books_With_Scenes.xml")]
    public void ChangeSceneBlockdBook()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "ChangeSceneBlockdBook";
        var homePage = Arrange.CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        Assert.NotNull(homePage);
        
        // given I have a book with a single scene block
        var bookSlug = "2e1f4ebf-b60b-4a10-ba0e-d751514c3841";
        Arrange.OpenPage(homePage, bookSlug);
        var page = homePage.SelectedPage;

        Assert.NotNull(page);
        Assert.NotNull(page.Scene);
        Assert.NotNull(page.Scene.Blocks);
        Assert.NotEmpty(page.Scene.Blocks);

        var block = page.Scene.Blocks.First();
        Assert.Equal("My single block", block.StringValue);

        // when I change the scene
        block.StringValue = "new test value";
        homePage.ApplyChanges();

        // when I save to file
        homePage.SaveToFile();

        // when I re-open the app
        homePage = Arrange.CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        Assert.NotNull(homePage);
        Arrange.OpenPage(homePage, bookSlug);

        // then the changes should be displayed
         page = homePage.SelectedPage;
        Assert.NotNull(page);
        Assert.NotNull(page.Scene);
        Assert.NotNull(page.Scene.Blocks);
        Assert.NotEmpty(page.Scene.Blocks);
        block = page.Scene.Blocks.First();
        Assert.Equal("new test value", block.StringValue);
    }
}
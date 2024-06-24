using bookeditor.ViewModels;

namespace bookeditor.test;

using Arrange = bookeditor.test.ViewModelArrangement;

public class UpdateBlocksList_Tests
{
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendBlockToEmptyScene.xml", "../../../TestData/Books_With_Scenes.xml")]
    public void AppendBlockToEmptyScene()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendBlockToEmptyScene";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book with no scene
        var bookSlug = "84723c01-b0f9-44a9-87b5-91beb090acaf";
        var title = "No scene";

        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Scene);

        // when I append a new block to the scene
        homePage.AppendSceneBlock();

        // then that scene is displayed with only the new block
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Blocks);
        Assert.NotEmpty(homePage.SelectedPage.Scene.Blocks);
        var block = homePage.SelectedPage.Scene.Blocks.First();
        Assert.Equal("text", block.BlockType);
        Assert.Equal("", block.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendSceneBlock.xml", "../../../TestData/Books_With_Scenes.xml")]
    public void AppendSceneBlock()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendSceneBlock";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book with a scene that has a block
        var  bookSlug = "2e1f4ebf-b60b-4a10-ba0e-d751514c3841";
        var title = "Single block scene";

        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Blocks);
        Assert.Single(homePage.SelectedPage.Scene.Blocks);
        var firstblock = homePage.SelectedPage.Scene.Blocks.First();
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My single block", firstblock.StringValue);

        // when I append a new block to the scene
        homePage.AppendSceneBlock();

        // then that scene is displayed with only the new block
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Blocks);
        Assert.NotEmpty(homePage.SelectedPage.Scene.Blocks);
        firstblock = homePage.SelectedPage.Scene.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My single block", firstblock.StringValue);
        firstblock = homePage.SelectedPage.Scene.Blocks[1];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("", firstblock.StringValue);
    }
    
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendToEmptyStoryBlock.xml", "../../../TestData/Books_With_Stories.xml")]
    public void AppendBlockToEmptyStory()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendToEmptyStoryBlock";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book with no story
        var  bookSlug = "0f19667e-283e-46df-b458-df77cdefc4bb";
        var title = "No story";
        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Story);

        // when I append a new block to the story
        homePage.AppendStoryBlock();

        // then that story is displayed with only the new block
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.NotEmpty(homePage.SelectedPage.Story.Blocks);
        var block = homePage.SelectedPage.Story.Blocks.First();
        Assert.Equal("text", block.BlockType);
        Assert.Equal("", block.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendStoryBlock.xml", "../../../TestData/Books_With_Stories.xml")]
    public void AppendStoryBlock()
    {        
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendStoryBlock";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book with a story that has a block
        var  bookSlug = "596d5e6a-3cfb-41af-b855-8bafa5a632f3";
        var title = "Single block story";
        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.Single(homePage.SelectedPage.Story.Blocks);
        var firstblock = homePage.SelectedPage.Story.Blocks.First();
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My single block", firstblock.StringValue);

        // when I append a new block to the story
        homePage.AppendStoryBlock();

        // then that story is displayed with only the new block
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.Equal(2, homePage.SelectedPage.Story.Blocks.Length);
        firstblock = homePage.SelectedPage.Story.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My single block", firstblock.StringValue);
        firstblock = homePage.SelectedPage.Story.Blocks[1];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("", firstblock.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/InsertBlockToEmptyStory.xml", "../../../TestData/Books_With_Stories.xml")]
    public void InsertBlockToEmptyStory()
    {        
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "InsertBlockToEmptyStory";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book with no story
        var  bookSlug = "0f19667e-283e-46df-b458-df77cdefc4bb";
        var title = "No story";
        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Story);

        // when I insert a block after the first
        homePage.InsertStoryBlockAfter(0);

        // then that story is displayed with only the new block
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.NotEmpty(homePage.SelectedPage.Story.Blocks);
        var block = homePage.SelectedPage.Story.Blocks.First();
        Assert.Equal("text", block.BlockType);
        Assert.Equal("", block.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/InsertStoryBlock.xml", "../../../TestData/Books_With_Stories.xml")]
    public void InsertStoryBlock()
    {        
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "InsertStoryBlock";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book with a story with many blocks
        var  bookSlug = "61814cd5-54f0-42ca-9e82-2195cd314abd";
        var title = "Many block story";
        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.Equal(3, homePage.SelectedPage.Story.Blocks.Length);

        var firstblock = homePage.SelectedPage.Story.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);
        
        var secondblock = homePage.SelectedPage.Story.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("My second block\n                ", secondblock.StringValue);
        
        var thirdblock = homePage.SelectedPage.Story.Blocks[2];
        Assert.Equal("text", thirdblock.BlockType);
        Assert.Equal("\nMy third block\n                ", thirdblock.StringValue);

        // when I insert a block after the first
        homePage.InsertStoryBlockAfter(0);

        // then that story is displayed with all the blocks in order
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.Equal(4, homePage.SelectedPage.Story.Blocks.Length);

        firstblock = homePage.SelectedPage.Story.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);

        secondblock = homePage.SelectedPage.Story.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("", secondblock.StringValue);
        
        thirdblock = homePage.SelectedPage.Story.Blocks[2];
        Assert.Equal("text", thirdblock.BlockType);
        Assert.Equal("My second block\n                ", thirdblock.StringValue);
        
        var fourthblock = homePage.SelectedPage.Story.Blocks[3];
        Assert.Equal("text", fourthblock.BlockType);
        Assert.Equal("\nMy third block\n                ", fourthblock.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/InsertStoryBlockWithIndexOutOfRange.xml", "../../../TestData/Books_With_Stories.xml")]
    public void InsertStoryBlockWithIndexOutOfRange()
    {        
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "InsertStoryBlockWithIndexOutOfRange";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book with a story with many blocks
        var  bookSlug = "61814cd5-54f0-42ca-9e82-2195cd314abd";
        var title = "Many block story";
        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.Equal(3, homePage.SelectedPage.Story.Blocks.Length);

        var firstblock = homePage.SelectedPage.Story.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);
        
        var secondblock = homePage.SelectedPage.Story.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("My second block\n                ", secondblock.StringValue);
        
        var thirdblock = homePage.SelectedPage.Story.Blocks[2];
        Assert.Equal("text", thirdblock.BlockType);
        Assert.Equal("\nMy third block\n                ", thirdblock.StringValue);

        // when I insert a block with an index greater than the length of the list
        homePage.InsertStoryBlockAfter(5);

        // then that story is displayed with the new block appended to the ordered list of blocks 
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.Equal(4, homePage.SelectedPage.Story.Blocks.Length);

        firstblock = homePage.SelectedPage.Story.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);
        
        secondblock = homePage.SelectedPage.Story.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("My second block\n                ", secondblock.StringValue);
        
        thirdblock = homePage.SelectedPage.Story.Blocks[2];
        Assert.Equal("text", thirdblock.BlockType);
        Assert.Equal("\nMy third block\n                ", thirdblock.StringValue);

        var fourthblock = homePage.SelectedPage.Story.Blocks[3];
        Assert.Equal("text", fourthblock.BlockType);
        Assert.Equal("", fourthblock.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/InsertBlockToEmptyScene.xml", "../../../TestData/Books_With_Scenes.xml")]
    public void InsertBlockToEmptyScene()
    {        
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "InsertBlockToEmptyScene";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book with no scene
        var  bookSlug = "84723c01-b0f9-44a9-87b5-91beb090acaf";
        var title = "No scene";
        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Scene); 

        // when I insert a block after the first
        homePage.InsertSceneBlockAfter(0);

        // then that scene is displayed with only the new block
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Blocks);
        Assert.NotEmpty(homePage.SelectedPage.Scene.Blocks);
        var block = homePage.SelectedPage.Scene.Blocks.First();
        Assert.Equal("text", block.BlockType);
        Assert.Equal("", block.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/InsertSceneBlock.xml", "../../../TestData/Books_With_Scenes.xml")]
    public void InsertSceneBlock()
    {        
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "InsertSceneBlock";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book with a scene with many blocks
        var  bookSlug = "156c2c19-abc8-4857-abb0-187c74c2d7f4";
        var title = "Many block scene";
        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Blocks);
        Assert.Equal(3, homePage.SelectedPage.Scene.Blocks.Length);

        var firstblock = homePage.SelectedPage.Scene.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);
        
        var secondblock = homePage.SelectedPage.Scene.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("My second block\n                ", secondblock.StringValue);
        
        var thirdblock = homePage.SelectedPage.Scene.Blocks[2];
        Assert.Equal("text", thirdblock.BlockType);
        Assert.Equal("\nMy third block\n                ", thirdblock.StringValue);

        // when I insert a block after the first
        homePage.InsertSceneBlockAfter(0);

        // then that scene is displayed with all the blocks in order
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Blocks);
        Assert.Equal(4, homePage.SelectedPage.Scene.Blocks.Length);

        firstblock = homePage.SelectedPage.Scene.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);

        secondblock = homePage.SelectedPage.Scene.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("", secondblock.StringValue);
        
        thirdblock = homePage.SelectedPage.Scene.Blocks[2];
        Assert.Equal("text", thirdblock.BlockType);
        Assert.Equal("My second block\n                ", thirdblock.StringValue);
        
        var fourthblock = homePage.SelectedPage.Scene.Blocks[3];
        Assert.Equal("text", fourthblock.BlockType);
        Assert.Equal("\nMy third block\n                ", fourthblock.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/InsertSceneBlock.xml", "../../../TestData/Books_With_Scenes.xml")]
    public void InsertSceneBlockWithIndexOutOfRange()
    {        
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "InsertSceneBlock";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book with a scene with many blocks
        var  bookSlug = "156c2c19-abc8-4857-abb0-187c74c2d7f4";
        var title = "Many block scene";
        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Blocks);
        Assert.Equal(3, homePage.SelectedPage.Scene.Blocks.Length);

        var firstblock = homePage.SelectedPage.Scene.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);
        
        var secondblock = homePage.SelectedPage.Scene.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("My second block\n                ", secondblock.StringValue);
        
        var thirdblock = homePage.SelectedPage.Scene.Blocks[2];
        Assert.Equal("text", thirdblock.BlockType);
        Assert.Equal("\nMy third block\n                ", thirdblock.StringValue);

        // when I insert a block with an index greater than the length of the list
        homePage.InsertSceneBlockAfter(5);

        // then that scene is displayed with the new block appended to the ordered list of blocks 
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Blocks);
        Assert.Equal(4, homePage.SelectedPage.Scene.Blocks.Length);

        firstblock = homePage.SelectedPage.Scene.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);
        
        secondblock = homePage.SelectedPage.Scene.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("My second block\n                ", secondblock.StringValue);
        
        thirdblock = homePage.SelectedPage.Scene.Blocks[2];
        Assert.Equal("text", thirdblock.BlockType);
        Assert.Equal("\nMy third block\n                ", thirdblock.StringValue);

        var fourthblock = homePage.SelectedPage.Scene.Blocks[3];
        Assert.Equal("text", fourthblock.BlockType);
        Assert.Equal("", fourthblock.StringValue);
    }
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/RemoveStoryBlock.xml", "../../../TestData/Books_With_Stories.xml")]
    public void RemoveStoryBlock()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "RemoveStoryBlock";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);
        Assert.NotNull(homePage);

        // given I have opened a book with a story with many blocks
        var bookSlug = "61814cd5-54f0-42ca-9e82-2195cd314abd";
        var title = "Many block story";
        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.Equal(3, homePage.SelectedPage.Story.Blocks.Length);

        var firstblock = homePage.SelectedPage.Story.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);
        
        var secondblock = homePage.SelectedPage.Story.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("My second block\n                ", secondblock.StringValue);
        
        var thirdblock = homePage.SelectedPage.Story.Blocks[2];
        Assert.Equal("text", thirdblock.BlockType);
        Assert.Equal("\nMy third block\n                ", thirdblock.StringValue);

        // when I delete the second block
        homePage.DeleteStoryBlock(1);

        // then that story is displayed without the second block but still in order
        Assert.NotNull(homePage.SelectedPage.Story);
        Assert.NotNull(homePage.SelectedPage.Story.Blocks);
        Assert.Equal(2, homePage.SelectedPage.Story.Blocks.Length);

        firstblock = homePage.SelectedPage.Story.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);

        secondblock = homePage.SelectedPage.Story.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("\nMy third block\n                ", secondblock.StringValue);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/RemoveSceneBlock.xml", "../../../TestData/Books_With_Scenes.xml")]
    public void RemoveSceneBlock()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "RemoveSceneBlock";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);
        Assert.NotNull(homePage);

        // given I have opened a book with a scene with many blocks
        var  bookSlug = "156c2c19-abc8-4857-abb0-187c74c2d7f4";
        var title = "Many block scene";

        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Blocks);
        Assert.Equal(3, homePage.SelectedPage.Scene.Blocks.Length);

        var firstblock = homePage.SelectedPage.Scene.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);
        
        var secondblock = homePage.SelectedPage.Scene.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("My second block\n                ", secondblock.StringValue);
        
        var thirdblock = homePage.SelectedPage.Scene.Blocks[2];
        Assert.Equal("text", thirdblock.BlockType);
        Assert.Equal("\nMy third block\n                ", thirdblock.StringValue);

        // when I delete the second block
        homePage.DeleteSceneBlock(1);

        // then that scene is displayed without the second block but still in order
        Assert.NotNull(homePage.SelectedPage.Scene);
        Assert.NotNull(homePage.SelectedPage.Scene.Blocks);
        Assert.Equal(2, homePage.SelectedPage.Scene.Blocks.Length);

        firstblock = homePage.SelectedPage.Scene.Blocks[0];
        Assert.Equal("text", firstblock.BlockType);
        Assert.Equal("My first block", firstblock.StringValue);

        secondblock = homePage.SelectedPage.Scene.Blocks[1];
        Assert.Equal("text", secondblock.BlockType);
        Assert.Equal("\nMy third block\n                ", secondblock.StringValue);
    }
}
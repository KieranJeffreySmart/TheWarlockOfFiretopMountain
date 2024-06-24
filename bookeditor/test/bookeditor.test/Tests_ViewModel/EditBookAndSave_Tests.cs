using bookeditor.ViewModels;

namespace bookeditor.test;

using Arrange = bookeditor.test.ViewModelArrangement;

public class EditBookAndSave_Tests
{
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/SaveChangesToSinglePage.xml", "../../../TestData/Books_With_Pages.xml")]
    public void SaveWithoutChanges()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "SaveChangesToSinglePage";
        var homePage = Arrange.CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        Assert.NotNull(homePage);
        
        // given I have opened a book with a page
        var bookSlug = "c6b3c0b5-4507-4e69-a2d7-0ad934413270";
        var pageSlug = "2de3bbc7-01e9-4b5a-a78f-ee7eea4eb9c0";
        var title = "Single page complete book";

        Arrange.OpenPage(homePage, bookSlug, p => p.Slug == pageSlug);
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        var page = homePage.SelectedPage;
        Assert.NotNull(page);
        Assert.Equal("Game", page.PageType);
        Assert.Equal(1, page.Index);

        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Blocks);
        Assert.Equal(6, page.Story.Blocks.Length);
        var storyblock1 = page.Story.Blocks[0];
        Assert.Equal("block 1", storyblock1.StringValue);
        var storyblock2 = page.Story.Blocks[1];
        Assert.Equal("block 2", storyblock2.StringValue);
        var storyblock3 = page.Story.Blocks[2];
        Assert.Equal("block 3", storyblock3.StringValue);
        var storyblock4 = page.Story.Blocks[3];
        Assert.Equal("./image1.jpg", storyblock4.StringValue);
        var storyblock5 = page.Story.Blocks[4];
        Assert.Equal("./image2.jpg", storyblock5.StringValue);
        var storyblock6 = page.Story.Blocks[5];
        Assert.Equal("./image3.jpg", storyblock6.StringValue);
        
        Assert.NotNull(page.Scene);
        Assert.NotNull(page.Scene.Blocks);
        Assert.Equal(6, page.Scene.Blocks.Length);
        var sceneblock1 = page.Scene.Blocks[0];
        Assert.Equal("block 1", sceneblock1.StringValue);
        var sceneblock2 = page.Scene.Blocks[1];
        Assert.Equal("block 2", sceneblock2.StringValue);
        var sceneblock3 = page.Scene.Blocks[2];
        Assert.Equal("block 3", sceneblock3.StringValue);
        var sceneblock4 = page.Scene.Blocks[3];
        Assert.Equal("./image1.jpg", sceneblock4.StringValue);
        var sceneblock5 = page.Scene.Blocks[4];
        Assert.Equal("./image2.jpg", sceneblock5.StringValue);
        var sceneblock6 = page.Scene.Blocks[5];
        Assert.Equal("./image3.jpg", sceneblock6.StringValue);
        
        Assert.NotNull(page.Options);
        Assert.Equal(7, page.Options.Length);

        var option1 = page.Options[0]; 
        Assert.Equal("s", option1.Key);  
        Assert.Equal("Start", option1.Label);  
        Assert.Equal("START_GAME", option1.Command);        
        var option2 = page.Options[1];
        Assert.Equal("q", option2.Key);  
        Assert.Equal("Quit", option2.Label);  
        Assert.Equal("QUIT_GAME", option2.Command); 
        var option3 = page.Options[2];
        Assert.Equal("c", option3.Key);  
        Assert.Equal("Continue", option3.Label);  
        Assert.Equal("NEXT_PAGE", option3.Command); 
        var option4 = page.Options[3];
        Assert.Equal("b", option4.Key);  
        Assert.Equal("Back", option4.Label);  
        Assert.Equal("PREVIOUS_PAGE", option4.Command); 
        var option5 = page.Options[4];
        Assert.Equal("g", option5.Key);  
        Assert.Equal("Go to Page 1", option5.Label);  
        Assert.Equal("GOTO_PAGE", option5.Command); 
        var option6 = page.Options[5];
        Assert.Equal("l", option6.Key);  
        Assert.Equal("Test yuor luck", option6.Label);
        Assert.Equal("TEST_STAT", option6.Command);
        var option7 = page.Options[6];
        Assert.Equal("f", option7.Key);  
        Assert.Equal("Fight", option7.Label);
        Assert.Equal("FIGHT_MONSTERS", option7.Command);

        // when I save to file
        homePage.SaveToFile();

        // when I re-open the app
        homePage = Arrange.CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);

        Arrange.OpenPage(homePage, bookSlug, p => p.Slug == pageSlug);
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        // then the changes should be displayed

        page = homePage.SelectedPage;
        Assert.NotNull(page);
        Assert.Equal("Game", page.PageType);
        Assert.Equal(1, page.Index);

        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Blocks);
        Assert.Equal(6, page.Story.Blocks.Length);
        storyblock1 = page.Story.Blocks[0];
        Assert.Equal("block 1", storyblock1.StringValue);
        storyblock2 = page.Story.Blocks[1];
        Assert.Equal("block 2", storyblock2.StringValue);
        storyblock3 = page.Story.Blocks[2];
        Assert.Equal("block 3", storyblock3.StringValue);
        storyblock4 = page.Story.Blocks[3];
        Assert.Equal("./image1.jpg", storyblock4.StringValue);
        storyblock5 = page.Story.Blocks[4];
        Assert.Equal("./image2.jpg", storyblock5.StringValue);
        storyblock6 = page.Story.Blocks[5];
        Assert.Equal("./image3.jpg", storyblock6.StringValue);
        
        Assert.NotNull(page.Scene);
        Assert.NotNull(page.Scene.Blocks);
        Assert.Equal(6, page.Scene.Blocks.Length);
        sceneblock1 = page.Scene.Blocks[0];
        Assert.Equal("block 1", sceneblock1.StringValue);
        sceneblock2 = page.Scene.Blocks[1];
        Assert.Equal("block 2", sceneblock2.StringValue);
        sceneblock3 = page.Scene.Blocks[2];
        Assert.Equal("block 3", sceneblock3.StringValue);
        sceneblock4 = page.Scene.Blocks[3];
        Assert.Equal("./image1.jpg", sceneblock4.StringValue);
        sceneblock5 = page.Scene.Blocks[4];
        Assert.Equal("./image2.jpg", sceneblock5.StringValue);
        sceneblock6 = page.Scene.Blocks[5];
        Assert.Equal("./image3.jpg", sceneblock6.StringValue);
        
        Assert.NotNull(page.Options);
        Assert.Equal(7, page.Options.Length);

        option1 = page.Options[0]; 
        Assert.Equal("s", option1.Key);  
        Assert.Equal("Start", option1.Label);  
        Assert.Equal("START_GAME", option1.Command);        
        option2 = page.Options[1];
        Assert.Equal("q", option2.Key);  
        Assert.Equal("Quit", option2.Label);  
        Assert.Equal("QUIT_GAME", option2.Command); 
        option3 = page.Options[2];
        Assert.Equal("c", option3.Key);  
        Assert.Equal("Continue", option3.Label);  
        Assert.Equal("NEXT_PAGE", option3.Command); 
        option4 = page.Options[3];
        Assert.Equal("b", option4.Key);  
        Assert.Equal("Back", option4.Label);  
        Assert.Equal("PREVIOUS_PAGE", option4.Command); 
        option5 = page.Options[4];
        Assert.Equal("g", option5.Key);  
        Assert.Equal("Go to Page 1", option5.Label);  
        Assert.Equal("GOTO_PAGE", option5.Command); 
        option6 = page.Options[5];
        Assert.Equal("l", option6.Key);  
        Assert.Equal("Test yuor luck", option6.Label);
        Assert.Equal("TEST_STAT", option6.Command);
        option7 = page.Options[6];
        Assert.Equal("f", option7.Key);  
        Assert.Equal("Fight", option7.Label);
        Assert.Equal("FIGHT_MONSTERS", option7.Command);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/SaveChangesToSinglePage.xml", "../../../TestData/Books_With_Pages.xml")]
    public void SaveChangesToSinglePage()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "SaveChangesToSinglePage";
        var homePage = Arrange.CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        Assert.NotNull(homePage);
        
        // given I have opened a book with a page
        var bookSlug = "c6b3c0b5-4507-4e69-a2d7-0ad934413270";
        var pageSlug = "2de3bbc7-01e9-4b5a-a78f-ee7eea4eb9c0";
        var title = "Single page complete book";

        Arrange.OpenPage(homePage, bookSlug, p => p.Slug == pageSlug);
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        var page = homePage.SelectedPage;
        Assert.NotNull(page);
        Assert.Equal("Game", page.PageType);
        Assert.Equal(1, page.Index);

        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Blocks);
        Assert.Equal(6, page.Story.Blocks.Length);
        var storyblock1 = page.Story.Blocks[0];
        Assert.Equal("block 1", storyblock1.StringValue);
        var storyblock2 = page.Story.Blocks[1];
        Assert.Equal("block 2", storyblock2.StringValue);
        var storyblock3 = page.Story.Blocks[2];
        Assert.Equal("block 3", storyblock3.StringValue);
        var storyblock4 = page.Story.Blocks[3];
        Assert.Equal("./image1.jpg", storyblock4.StringValue);
        var storyblock5 = page.Story.Blocks[4];
        Assert.Equal("./image2.jpg", storyblock5.StringValue);
        var storyblock6 = page.Story.Blocks[5];
        Assert.Equal("./image3.jpg", storyblock6.StringValue);
        
        Assert.NotNull(page.Scene);
        Assert.NotNull(page.Scene.Blocks);
        Assert.Equal(6, page.Scene.Blocks.Length);
        var sceneblock1 = page.Scene.Blocks[0];
        Assert.Equal("block 1", sceneblock1.StringValue);
        var sceneblock2 = page.Scene.Blocks[1];
        Assert.Equal("block 2", sceneblock2.StringValue);
        var sceneblock3 = page.Scene.Blocks[2];
        Assert.Equal("block 3", sceneblock3.StringValue);
        var sceneblock4 = page.Scene.Blocks[3];
        Assert.Equal("./image1.jpg", sceneblock4.StringValue);
        var sceneblock5 = page.Scene.Blocks[4];
        Assert.Equal("./image2.jpg", sceneblock5.StringValue);
        var sceneblock6 = page.Scene.Blocks[5];
        Assert.Equal("./image3.jpg", sceneblock6.StringValue);
        
        Assert.NotNull(page.Options);
        Assert.Equal(7, page.Options.Length);

        var option1 = page.Options[0]; 
        Assert.Equal("s", option1.Key);  
        Assert.Equal("Start", option1.Label);  
        Assert.Equal("START_GAME", option1.Command);        
        var option2 = page.Options[1];
        Assert.Equal("q", option2.Key);  
        Assert.Equal("Quit", option2.Label);  
        Assert.Equal("QUIT_GAME", option2.Command); 
        var option3 = page.Options[2];
        Assert.Equal("c", option3.Key);  
        Assert.Equal("Continue", option3.Label);  
        Assert.Equal("NEXT_PAGE", option3.Command); 
        var option4 = page.Options[3];
        Assert.Equal("b", option4.Key);  
        Assert.Equal("Back", option4.Label);  
        Assert.Equal("PREVIOUS_PAGE", option4.Command); 
        var option5 = page.Options[4];
        Assert.Equal("g", option5.Key);  
        Assert.Equal("Go to Page 1", option5.Label);  
        Assert.Equal("GOTO_PAGE", option5.Command); 
        var option6 = page.Options[5];
        Assert.Equal("l", option6.Key);  
        Assert.Equal("Test yuor luck", option6.Label);
        Assert.Equal("TEST_STAT", option6.Command);
        var option7 = page.Options[6];
        Assert.Equal("f", option7.Key);  
        Assert.Equal("Fight", option7.Label);
        Assert.Equal("FIGHT_MONSTERS", option7.Command);

        // when I change the page        
        storyblock1.StringValue = "changed story block 1";
        homePage.AppendStoryBlock();
        var appenedstoryblock = page.Story.Blocks.Last();
        appenedstoryblock.StringValue = "appended story block";
        homePage.InsertStoryBlockAfter(0);
        var insertstoryblock = page.Story.Blocks[1];
        insertstoryblock.StringValue = "inserted story block";
        homePage.DeleteStoryBlock(3);

        sceneblock1.StringValue = "changed scene block 1";
        homePage.AppendSceneBlock();
        var appenedsceneblock = page.Scene.Blocks.Last();
        appenedsceneblock.StringValue = "appended scene block";
        homePage.InsertSceneBlockAfter(0);
        var insertsceneblock = page.Scene.Blocks[1];
        insertsceneblock.StringValue = "inserted scene block";
        homePage.DeleteSceneBlock(3);

        option1.Label = "Changed start game";

        option2.Key = "x";

        homePage.AppendOptionByCommand("NEW_OPTION");
        var appendOption = page.Options.Last();
        appendOption.Key = "n";
        appendOption.Label = "New option";
        homePage.DeleteOption(4);
        homePage.ApplyChanges();

        // when I save to file
        homePage.SaveToFile();

        // when I re-open the app
        homePage = Arrange.CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        Arrange.OpenPage(homePage, bookSlug, p => p.Slug == pageSlug);
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        // then the changes should be displayed
        page = homePage.SelectedPage;
        Assert.NotNull(page);
        Assert.Equal("Game", page.PageType);
        Assert.Equal(1, page.Index);

        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Blocks);
        Assert.Equal(7, page.Story.Blocks.Length);
        storyblock1 = page.Story.Blocks[0];
        Assert.Equal("changed story block 1", storyblock1.StringValue);
        storyblock2 = page.Story.Blocks[1];
        Assert.Equal("inserted story block", storyblock2.StringValue);
        storyblock3 = page.Story.Blocks[2];
        Assert.Equal("block 2", storyblock3.StringValue);
        storyblock4 = page.Story.Blocks[3];
        Assert.Equal("./image1.jpg", storyblock4.StringValue);
        storyblock5 = page.Story.Blocks[4];
        Assert.Equal("./image2.jpg", storyblock5.StringValue);
        storyblock6 = page.Story.Blocks[5];
        Assert.Equal("./image3.jpg", storyblock6.StringValue);
        var storyblock7 = page.Story.Blocks[6];
        Assert.Equal("appended story block", storyblock7.StringValue);

        
        Assert.NotNull(page.Scene);
        Assert.NotNull(page.Scene.Blocks);
        Assert.Equal(7, page.Scene.Blocks.Length);
        sceneblock1 = page.Scene.Blocks[0];
        Assert.Equal("changed scene block 1", sceneblock1.StringValue);
        sceneblock2 = page.Scene.Blocks[1];
        Assert.Equal("inserted scene block", sceneblock2.StringValue);
        sceneblock3 = page.Scene.Blocks[2];
        Assert.Equal("block 2", sceneblock3.StringValue);
        sceneblock4 = page.Scene.Blocks[3];
        Assert.Equal("./image1.jpg", sceneblock4.StringValue);
        sceneblock5 = page.Scene.Blocks[4];
        Assert.Equal("./image2.jpg", sceneblock5.StringValue);
        sceneblock6 = page.Scene.Blocks[5];
        Assert.Equal("./image3.jpg", sceneblock6.StringValue);
        var sceneblock7 = page.Scene.Blocks[6];
        Assert.Equal("appended scene block", sceneblock7.StringValue);

        
        Assert.NotNull(page.Options);
        Assert.Equal(7, page.Options.Length);

        option1 = page.Options[0]; 
        Assert.Equal("s", option1.Key);  
        Assert.Equal("Changed start game", option1.Label);  
        Assert.Equal("START_GAME", option1.Command);        
        option2 = page.Options[1];
        Assert.Equal("x", option2.Key);  
        Assert.Equal("Quit", option2.Label);  
        Assert.Equal("QUIT_GAME", option2.Command); 
        option3 = page.Options[2];
        Assert.Equal("c", option3.Key);  
        Assert.Equal("Continue", option3.Label);  
        Assert.Equal("NEXT_PAGE", option3.Command); 
        option4 = page.Options[3];
        Assert.Equal("b", option4.Key);  
        Assert.Equal("Back", option4.Label);  
        Assert.Equal("PREVIOUS_PAGE", option4.Command); 
        option5 = page.Options[4];
        Assert.Equal("l", option6.Key);  
        Assert.Equal("Test yuor luck", option6.Label);
        Assert.Equal("TEST_STAT", option6.Command);
        option6 = page.Options[5];
        Assert.Equal("f", option7.Key);  
        Assert.Equal("Fight", option7.Label);
        Assert.Equal("FIGHT_MONSTERS", option7.Command);
        option7 = page.Options[6];
        Assert.Equal("n", option7.Key);  
        Assert.Equal("New option", option7.Label);
        Assert.Equal("NEW_OPTION", option7.Command);
    }
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/SaveChangesToManyPages.xml", "../../../TestData/Books_With_Stories.xml")]
    public void SaveChangesToManyPages()
    {
        // given I have a library
    }
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/SaveChangesToManyPages.xml", "../../../TestData/Books_With_Stories.xml")]
    public void SaveNewPages()
    {
        // given I have a library
    }
    
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/SaveChangesToManyPages.xml", "../../../TestData/Books_With_Stories.xml")]
    public void SaveRemovedPages()
    {
        // given I have a library
    }
}

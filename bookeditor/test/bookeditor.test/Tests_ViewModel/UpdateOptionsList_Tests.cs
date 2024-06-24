using System.Reflection;
using bookeditor.ViewModels;
using Arrange = bookeditor.test.ViewModelArrangement;

namespace bookeditor.test;

public class UpdateOptionsList_Tests
{
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendOptionToPageWithNoOptions.xml", "../../../TestData/Books_With_Options.xml")]
    public void AppendOptionToPageWithNoOptions()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendOptionToPageWithNoOptions";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book to a page with no options
        var  bookSlug = "690fdb06-a334-4d33-8e5e-3c45a8bb87cb";
        var title = "No options";

        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Options); 

        // when I append an option
        homePage.AppendOptionByCommand("NEW_OPTION");

        // then the page is displayed with only the new option
        Assert.NotNull(homePage.SelectedPage.Options); 
        Assert.Single(homePage.SelectedPage.Options); 
        var option = homePage.SelectedPage.Options.First();
        Assert.Equal("NEW_OPTION", option.Command);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendOptionToPageWithSingleOption.xml", "../../../TestData/Books_With_Options.xml")]
    public void AppendOptionToPageWithSingleOption()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendOptionToPageWithSingleOption";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book to a page with a single option
        var  bookSlug = "961dc709-f38a-48d8-97b9-a89deb964ef1";
        var title = "Quit Command";

        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Single(homePage.SelectedPage.Options); 
        var option1 = homePage.SelectedPage.Options.First();
        Assert.Equal("QUIT_GAME", option1.Command);

        // when I append an option
        homePage.AppendOptionByCommand("NEW_OPTION");

        // then the page is displayed with both options in order
        Assert.NotNull(homePage.SelectedPage.Options); 
        Assert.Equal(2, homePage.SelectedPage.Options.Length); 
        option1 = homePage.SelectedPage.Options[0];
        Assert.Equal("QUIT_GAME", option1.Command);
        var option2 = homePage.SelectedPage.Options[1];
        Assert.Equal("NEW_OPTION", option2.Command);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendQuitOptionToPageWithManyOptions.xml", "../../../TestData/Books_With_Options.xml")]
    public void AppendQuitOptionToPageWithManyOptions()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendQuitOptionToPageWithManyOptions";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book to a page with many options
        var  bookSlug = "a7ea18f6-a18b-4626-af9e-543d5227e6e8";
        var pageSlug = "4f5e2e27-3a75-4ebc-8ec9-f4612394fb4c";
        var title = "Goto Continue and Back";

        Arrange.OpenPage(homePage, bookSlug, p => p.Slug == pageSlug );
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);
        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal(3, homePage.SelectedPage.Options.Length);
        var option1 = homePage.SelectedPage.Options[0];
        Assert.Equal("GOTO_PAGE", option1.Command);
        var option2 = homePage.SelectedPage.Options[1];
        Assert.Equal("NEXT_PAGE", option2.Command);
        var option3 = homePage.SelectedPage.Options[2];
        Assert.Equal("PREVIOUS_PAGE", option3.Command);

        // when I append an option
        homePage.AppendOptionByCommand("NEW_OPTION");

        // then the page is displayed with all options in order
        Assert.NotNull(homePage.SelectedPage.Options); 
        Assert.Equal(4, homePage.SelectedPage.Options.Length); 
        option1 = homePage.SelectedPage.Options[0];
        Assert.Equal("GOTO_PAGE", option1.Command);
        option2 = homePage.SelectedPage.Options[1];
        Assert.Equal("NEXT_PAGE", option2.Command);
        option3 = homePage.SelectedPage.Options[2];
        Assert.Equal("PREVIOUS_PAGE", option3.Command);
        var option4 = homePage.SelectedPage.Options[3];
        Assert.Equal("NEW_OPTION", option4.Command);
    }
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/RemoveOptionFromPageWithSingleOption.xml", "../../../TestData/Books_With_Options.xml")]
    public void RemoveOptionFromPageWithSingleOption()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "RemoveOptionFromPageWithSingleOption";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book to a page with a single option
        var  bookSlug = "961dc709-f38a-48d8-97b9-a89deb964ef1";
        var title = "Quit Command";

        Arrange.OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Single(homePage.SelectedPage.Options); 
        var option1 = homePage.SelectedPage.Options.First();
        Assert.Equal("QUIT_GAME", option1.Command);

        // when I remove the option
        homePage.DeleteOption(0);

        // then the page is displayed with no options
        Assert.NotNull(homePage.SelectedPage);
        Assert.Empty(homePage.SelectedPage.Options); 
    }
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/RemoveOptionFromPageWithManyOptions.xml", "../../../TestData/Books_With_Options.xml")]
    public void RemoveOptionFromPageWithManyOptions()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "RemoveOptionFromPageWithManyOptions";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book to a page with many options
        var  bookSlug = "a7ea18f6-a18b-4626-af9e-543d5227e6e8";
        var pageSlug = "4f5e2e27-3a75-4ebc-8ec9-f4612394fb4c";
        var title = "Goto Continue and Back";
        
        Arrange.OpenPage(homePage, bookSlug, p => p.Slug == pageSlug );
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);
        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal(3, homePage.SelectedPage.Options.Length);
        var option1 = homePage.SelectedPage.Options[0];
        Assert.Equal("GOTO_PAGE", option1.Command);
        var option2 = homePage.SelectedPage.Options[1];
        Assert.Equal("NEXT_PAGE", option2.Command);
        var option3 = homePage.SelectedPage.Options[2];
        Assert.Equal("PREVIOUS_PAGE", option3.Command);

        // when I remove the first option
        homePage.DeleteOption(0);

        // then the page is displayed with only options after the first
        Assert.NotNull(homePage.SelectedPage);
        Assert.Equal(2, homePage.SelectedPage.Options.Length);
        option1 = homePage.SelectedPage.Options[0];
        Assert.Equal("NEXT_PAGE", option1.Command);
        option2 = homePage.SelectedPage.Options[1];
        Assert.Equal("PREVIOUS_PAGE", option2.Command);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/RemoveOptionFromPageWithIndexOutOfBounds.xml", "../../../TestData/Books_With_Options.xml")]
    public void RemoveOptionFromPageWithIndexOutOfBounds()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "RemoveOptionFromPageWithIndexOutOfBounds";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book to a page with many options
        var bookSlug = "a7ea18f6-a18b-4626-af9e-543d5227e6e8";
        var pageSlug = "4f5e2e27-3a75-4ebc-8ec9-f4612394fb4c";
        var title = "Goto Continue and Back";
        
        Arrange.OpenPage(homePage, bookSlug, p => p.Slug == pageSlug );
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);
        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal(3, homePage.SelectedPage.Options.Length);
        var option1 = homePage.SelectedPage.Options[0];
        Assert.Equal("GOTO_PAGE", option1.Command);
        var option2 = homePage.SelectedPage.Options[1];
        Assert.Equal("NEXT_PAGE", option2.Command);
        var option3 = homePage.SelectedPage.Options[2];
        Assert.Equal("PREVIOUS_PAGE", option3.Command);

        // when I remove an option using an index greater than the length of the options list
        void deleteOption() => homePage.DeleteOption(4);

        // then I am informed of an error
        var exception = Assert.Throws<Exception>(deleteOption);
        Assert.Equal("Cannot delete something that doesnt exist", exception.Message);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/RemoveSelectedOptionFromPage.xml", "../../../TestData/Books_With_Options.xml")]
    public void RemoveSelectedOptionFromPage()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "RemoveSelectedOptionFromPage";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book to a page with many options
        var bookSlug = "a7ea18f6-a18b-4626-af9e-543d5227e6e8";
        var pageSlug = "4f5e2e27-3a75-4ebc-8ec9-f4612394fb4c";
        var title = "Goto Continue and Back";
        
        Arrange.OpenPage(homePage, bookSlug, p => p.Slug == pageSlug );
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);
        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal(3, homePage.SelectedPage.Options.Length);
        var option1 = homePage.SelectedPage.Options[0];
        Assert.Equal("GOTO_PAGE", option1.Command);
        var option2 = homePage.SelectedPage.Options[1];
        Assert.Equal("NEXT_PAGE", option2.Command);
        var option3 = homePage.SelectedPage.Options[2];
        Assert.Equal("PREVIOUS_PAGE", option3.Command);

        // given I have selected the second option
        homePage.SelectOption(option2);
        Assert.NotNull(homePage.SelectedOption);
        Assert.Equal("NEXT_PAGE", homePage.SelectedOption.Command);

        // when I remove an option using an index greater than the length of the options list
        homePage.DeleteSelectedOption();

        // then I am informed of an error
        Assert.NotNull(homePage.SelectedPage);
        Assert.Equal(2, homePage.SelectedPage.Options.Length);
        option1 = homePage.SelectedPage.Options[0];
        Assert.Equal("GOTO_PAGE", option1.Command);
        option2 = homePage.SelectedPage.Options[1];
        Assert.Equal("PREVIOUS_PAGE", option2.Command);
        Assert.Null(homePage.SelectedOption);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/RemoveSelectedOptionFromPageWithNoOptionSelected.xml", "../../../TestData/Books_With_Options.xml")]
    public void RemoveSelectedOptionFromPageWithNoOptionSelected()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "RemoveSelectedOptionFromPageWithNoOptionSelected";
        BookEditorHomeViewModel homePage = Arrange.CreateHomePageVM(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book to a page with many options
        var bookSlug = "a7ea18f6-a18b-4626-af9e-543d5227e6e8";
        var pageSlug = "4f5e2e27-3a75-4ebc-8ec9-f4612394fb4c";
        var title = "Goto Continue and Back";
        
        Arrange.OpenPage(homePage, bookSlug, p => p.Slug == pageSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);
        Assert.NotNull(homePage.SelectedPage);
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Equal(3, homePage.SelectedPage.Options.Length);
        var option1 = homePage.SelectedPage.Options[0];
        Assert.Equal("GOTO_PAGE", option1.Command);
        var option2 = homePage.SelectedPage.Options[1];
        Assert.Equal("NEXT_PAGE", option2.Command);
        var option3 = homePage.SelectedPage.Options[2];
        Assert.Equal("PREVIOUS_PAGE", option3.Command);


        // when I remove an option using an index greater than the length of the options list
        void deleteOption() => homePage.DeleteSelectedOption();

        // then I am informed of an error
        var exception = Assert.Throws<Exception>(deleteOption);
        Assert.Equal("Cannot delete something that doesnt exist", exception.Message);
    }
}
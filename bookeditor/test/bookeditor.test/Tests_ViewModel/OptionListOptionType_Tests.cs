using bookeditor.ViewModels;

namespace bookeditor.test;

public class OptionListOptionType_Tests
{
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendAnEmptyOption.xml", "../../../TestData/Books_With_Options.xml")]
    public void AppendAnEmptyOption()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendAnEmptyOption";
        BookEditorHomeViewModel homePage = CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book to a page with no options
        var  bookSlug = "690fdb06-a334-4d33-8e5e-3c45a8bb87cb";
        var title = "No options";

        OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Options);

        // when I append an option with an unknown command
        homePage.AppendOptionByCommand();

        // then the option is displayed with 
        // and option has an empty command
        // and option has a an empty key 
        // and option has a label with the command name 
        // and option has empty argument and outcome lists
        Assert.NotNull(homePage.SelectedPage.Options); 
        Assert.Single(homePage.SelectedPage.Options); 
        var option = homePage.SelectedPage.Options.First();
        Assert.Equal("", option.Command);
        Assert.Equal(string.Empty, option.Key);
        Assert.Equal("", option.Label);
        Assert.NotNull(option.Arguments);
        Assert.NotNull(option.Outcomes);
        Assert.Empty(option.Arguments);
        Assert.Empty(option.Outcomes);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendAnUnknownOption.xml", "../../../TestData/Books_With_Options.xml")]
    public void AppendAnUnknownOption()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendAnUnknownOption";
        BookEditorHomeViewModel homePage = CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);

        // given I have opened a book to a page with no options
        var  bookSlug = "690fdb06-a334-4d33-8e5e-3c45a8bb87cb";
        var title = "No options";

        OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Options);

        // when I append an option with an unknown command
        homePage.AppendOptionByCommand("UNKNOWN_OPTION");

        // then the option is displayed with 
        // and option has the unknown command
        // and an empty key 
        // and label with the command name 
        // and empty argument and outcome lists
        Assert.NotNull(homePage.SelectedPage.Options); 
        Assert.Single(homePage.SelectedPage.Options); 
        var option = homePage.SelectedPage.Options.First();
        Assert.Equal("UNKNOWN_OPTION", option.Command);
        Assert.Equal(string.Empty, option.Key);
        Assert.Equal("UNKNOWN_OPTION", option.Label);
        Assert.NotNull(option.Arguments);
        Assert.NotNull(option.Outcomes);
        Assert.Empty(option.Arguments);
        Assert.Empty(option.Outcomes);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendAStartOption.xml", "../../../TestData/Books_With_Options.xml")]
    public void AppendAStartOption()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendAStartOption";
        BookEditorHomeViewModel homePage = CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        
        // given I have opened a book to a page with no options
        var  bookSlug = "690fdb06-a334-4d33-8e5e-3c45a8bb87cb";
        var title = "No options";

        OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Options);

        // when I append an option with a start command
        homePage.AppendOptionByCommand("START_GAME");

        // then the page is displayed with only the Start game option
        // and the option has a start game command
        // and a default key of s and default label Start game
        // and empty arguments and outcomes
        Assert.NotNull(homePage.SelectedPage.Options); 
        Assert.Single(homePage.SelectedPage.Options); 
        var option = homePage.SelectedPage.Options.First();
        Assert.Equal("START_GAME", option.Command);

        Assert.Equal("s", option.Key);
        Assert.Equal("Start game", option.Label);

        Assert.NotNull(option.Arguments);
        Assert.NotNull(option.Outcomes);
        Assert.Empty(option.Arguments);
        Assert.Empty(option.Outcomes);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendQuitOption.xml", "../../../TestData/Books_With_Options.xml")]
    public void AppendQuitOption()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendQuitOption";
        BookEditorHomeViewModel homePage = CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        
        // given I have opened a book to a page with no options
        var  bookSlug = "690fdb06-a334-4d33-8e5e-3c45a8bb87cb";
        var title = "No options";

        OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Options);

        // when I append an option with the quit game command
        homePage.AppendOptionByCommand("QUIT_GAME");

        // then the page is displayed with only the quit game option
        // and the option has a quit game command
        // and a default key of q and default label Quit game
        // and empty arguments and outcomes
        Assert.NotNull(homePage.SelectedPage.Options); 
        Assert.Single(homePage.SelectedPage.Options); 
        var option = homePage.SelectedPage.Options.First();
        Assert.Equal("QUIT_GAME", option.Command);

        Assert.Equal("q", option.Key);
        Assert.Equal("Quit game", option.Label);

        Assert.NotNull(option.Arguments);
        Assert.NotNull(option.Outcomes);
        Assert.Empty(option.Arguments);
        Assert.Empty(option.Outcomes);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendNextPageOption.xml", "../../../TestData/Books_With_Options.xml")]
    public void AppendNextPageOption()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendNextPageOption";
        BookEditorHomeViewModel homePage = CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        
        // given I have opened a book to a page with no options
        var  bookSlug = "690fdb06-a334-4d33-8e5e-3c45a8bb87cb";
        var title = "No options";

        OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Options);

        // when I append an option with the next page command
        homePage.AppendOptionByCommand("NEXT_PAGE");

        // then the page is displayed with only the next page option
        // and the option has a next page command
        // and a default key of n and default label Next page
        // and empty arguments and outcomes
        Assert.NotNull(homePage.SelectedPage.Options); 
        Assert.Single(homePage.SelectedPage.Options); 
        var option = homePage.SelectedPage.Options.First();
        Assert.Equal("NEXT_PAGE", option.Command);

        Assert.Equal("n", option.Key);
        Assert.Equal("Next page", option.Label);

        Assert.NotNull(option.Arguments);
        Assert.NotNull(option.Outcomes);
        Assert.Empty(option.Arguments);
        Assert.Empty(option.Outcomes);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendPreviousPageOption.xml", "../../../TestData/Books_With_Options.xml")]
    public void AppendPreviousPageOption()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendPreviousPageOption";
        BookEditorHomeViewModel homePage = CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        
        // given I have opened a book to a page with no options
        var  bookSlug = "690fdb06-a334-4d33-8e5e-3c45a8bb87cb";
        var title = "No options";

        OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Options);

        // when I append an option
        homePage.AppendOptionByCommand("PREVIOUS_PAGE");

        // then the page is displayed with only the previous page option
        // and the option has a previous page command
        // and a default key of p and default label Previous page
        // and empty arguments and outcomes
        Assert.NotNull(homePage.SelectedPage.Options); 
        Assert.Single(homePage.SelectedPage.Options); 
        var option = homePage.SelectedPage.Options.First();
        Assert.Equal("PREVIOUS_PAGE", option.Command);

        Assert.Equal("p", option.Key);
        Assert.Equal("Previous page", option.Label);

        Assert.NotNull(option.Arguments);
        Assert.NotNull(option.Outcomes);
        Assert.Empty(option.Arguments);
        Assert.Empty(option.Outcomes);
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendGotoOption.xml", "../../../TestData/Books_With_Options.xml")]
    public void AppendGotoOption()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "AppendGotoOption";
        BookEditorHomeViewModel homePage = CreateLibrary(rootPath, [defaultLibrary], defaultLibrary);
        
        // given I have opened a book to a page with no options
        var  bookSlug = "690fdb06-a334-4d33-8e5e-3c45a8bb87cb";
        var title = "No options";

        OpenPage(homePage, bookSlug);
        Assert.NotNull(homePage.SelectedBook);
        Assert.Equal(title, homePage.SelectedBook.Title);

        Assert.NotNull(homePage.SelectedPage);
        Assert.Null(homePage.SelectedPage.Options);

        // when I append an option with a go to page command
        homePage.AppendOptionByCommand("GOTO_PAGE");
        
        // then the page is displayed with only the go to page option
        // and the option has a go to page command
        // and the option has a default key of g and default label Go to page
        // and the options arguments have a single argument named page with a default value of 1
        // and outcomes are empty
        Assert.NotNull(homePage.SelectedPage.Options);
        Assert.Single(homePage.SelectedPage.Options); 
        var option = homePage.SelectedPage.Options.First();
        Assert.Equal("GOTO_PAGE", option.Command);

        Assert.Equal("g", option.Key);
        Assert.Equal("Go to page", option.Label);

        Assert.NotNull(option.Arguments);
        Assert.Single(option.Arguments);
        Assert.Equal("page", option.Arguments.First().Key);
        Assert.Equal("1", option.Arguments.First().Value);

        Assert.NotNull(option.Outcomes);
        Assert.Empty(option.Outcomes);
    }

    [Fact]
    public void AppendStatTestOption()
    {
        // given I have a library
        // given I have opened a book to a page with a single option
        // when I append an option
        // then the page is displayed with only the new option
    }

    [Fact]
    public void AppendFightOption()
    {
        // given I have a library
        // given I have opened a book to a page with a single option
        // when I append an option
        // then the page is displayed with only the new option
    }

    private static void OpenPage(BookEditorHomeViewModel homePage, string bookSlug, Func<Page, bool>? predicate = null)
    {
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Slug == bookSlug);
        homePage.SelectedBook = book;        
        Assert.NotNull(homePage.SelectedBook);
        Assert.NotNull(homePage.SelectedBook.Pages);
        Assert.NotEmpty(homePage.SelectedBook.Pages);

        homePage.SelectedPage = predicate != null ? homePage.SelectedBook.Pages.First(predicate) : homePage.SelectedBook.Pages.First();
    }

    private static BookEditorHomeViewModel CreateLibrary(string rootPath, string[] libraryNames, string defaultLibrary = "New_Test_Library")
    {
        var library = new XmlLibrary(rootPath, libraryNames)
        {
            DefaultLibraryName = defaultLibrary
        };

        var cache = new EditorStateCache();

        return new BookEditorHomeViewModel(library, cache);
    }

}
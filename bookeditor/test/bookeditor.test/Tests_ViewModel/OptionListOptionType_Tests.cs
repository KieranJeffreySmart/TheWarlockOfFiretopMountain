using bookeditor.ViewModels;

namespace bookeditor.test;

public class OptionListOptionType_Tests
{
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

        // then the option is displayed with default vlues for key and label and empty argument and outcome lists
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
    public void AppendAStartOption()
    {
        // given I have a library
        // given I have opened a book to a page with no options
        // when I append an option
        // then the page is displayed with only the new option
    }

    [Fact]
    public void AppendQuitOption()
    {
        // given I have a library
        // given I have opened a book to a page with a single option
        // when I append an option
        // then the page is displayed with only the new option
    }

    [Fact]
    public void AppendContinueOption()
    {
        // given I have a library
        // given I have opened a book to a page with a single option
        // when I append an option
        // then the page is displayed with only the new option
    }

    [Fact]
    public void AppendBackOption()
    {
        // given I have a library
        // given I have opened a book to a page with a single option
        // when I append an option
        // then the page is displayed with only the new option
    }

    [Fact]
    public void AppendGotoOption()
    {
        // given I have a library
        // given I have opened a book to a page with a single option
        // when I append an option
        // then the page is displayed with only the new option
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
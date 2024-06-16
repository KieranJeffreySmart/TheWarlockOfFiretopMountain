using bookeditor.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace bookeditor.test;

public class ViewOptions_Tests 
{

    [Theory]
    [InlineData("Start Command", 0, 1, new[] {"s"}, new[] {"Start"}, new[] {"START_GAME"})]
    [InlineData("Quit Command", 0, 1, new[] {"q"}, new[] {"Quit"}, new[] {"QUIT_GAME"})]
    [InlineData("Continue Command", 0, 1, new[] {"c"}, new[] {"Continue"}, new[] {"NEXT_PAGE"})]
    [InlineData("Continue and Back Commands", 0, 1, new[] {"c"}, new[] {"Continue"}, new[] {"NEXT_PAGE"})]
    [InlineData("Continue and Back Commands", 1, 1, new[] {"b"}, new[] {"Back"}, new[] {"PREVIOUS_PAGE"})]

    public async Task ViewSimplePageOptions(string testBook, int pageArrayIndex, int optionCount, string[] optionsKeys, string[] labels, string[] commands)
    {
        // Given I have a library
        var libraryName = "Books_With_Options";
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
        viewModel.SelectedPage = viewModel.SelectedBook.Pages[pageArrayIndex];
        viewModel.UpdateSelectedPage();

        // then the options are displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPageDetails);
        Assert.NotNull(viewModel.SelectedPageDetails.Page);
        Assert.Equal(optionCount, viewModel.SelectedPageDetails.Page.Options.Length);
        Assert.Equal(optionsKeys, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Key).ToArray());
        Assert.Equal(labels, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Label).ToArray());
        Assert.Equal(commands, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Command).ToArray());
    }
    

    [Theory]
    [InlineData("Goto Command", 0, 1, new[] {"g"}, new[] {"Go to Page 3"}, new[] {"GOTO_GAME_PAGE"}, new[] {"page"}, new[] {"3"})]
    [InlineData("Goto Command", 1, 2, new[] {"g", "g"}, new[] {"Go to Page 1", "Go to Page 3"}, new[] {"GOTO_GAME_PAGE", "GOTO_GAME_PAGE"}, new[] {"page", "page"}, new[] {"1", "3"})]
    [InlineData("Goto Continue and Back", 1, 3, new[] {"g", "c", "b"}, new[] {"Go to Page 1", "Continue", "Back"}, new[] {"GOTO_GAME_PAGE", "NEXT_PAGE", "PREVIOUS_PAGE"}, new[] {"page"}, new[] {"1"})]

    public async Task ViewPageOptionsWithArguments(string testBook, int pageArrayIndex, int optionCount, string[] optionsKeys, string[] labels, string[] commands, string[] argNames, string[] argValues)
    {
        // Given I have a library
        var libraryName = "Books_With_Options";
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
        viewModel.SelectedPage = viewModel.SelectedBook.Pages[pageArrayIndex];
        viewModel.UpdateSelectedPage();

        // then the options are displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPageDetails);
        Assert.NotNull(viewModel.SelectedPageDetails.Page);
        Assert.Equal(optionCount, viewModel.SelectedPageDetails.Page.Options.Length);
        Assert.Equal(optionsKeys, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Key).ToArray());
        Assert.Equal(labels, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Label).ToArray());
        Assert.Equal(commands, viewModel.SelectedPageDetails.Page.Options.Select(o => o.Command).ToArray());
        Assert.Equal(argNames, viewModel.SelectedPageDetails.Page.Options.Where(o => o.Arguments != null).SelectMany(o => o.Arguments.Select(a => a.Key)).ToArray());
        Assert.Equal(argValues, viewModel.SelectedPageDetails.Page.Options.Where(o => o.Arguments != null).SelectMany(o => o.Arguments.Select(a => a.Value)).ToArray());
    }



    [Fact]
    public async Task ViewStatCheckOptions()
    {
        // Given I have a library
        var libraryName = "Books_With_Options";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
        await viewModel.Init();
        Assert.NotNull(viewModel.Books);
        viewModel.SelectedBook = viewModel.Books.First(b => b.Title == "Stats test");
        viewModel.UpdateSelectedBook();
        
        // Then there should be pages displayed
        Assert.NotNull(viewModel.SelectedBook);
        Assert.NotNull(viewModel.SelectedBook.Pages);
        Assert.NotEmpty(viewModel.SelectedBook.Pages);

        // When the page is selected
        viewModel.SelectedPage = viewModel.SelectedBook.Pages[0];
        viewModel.UpdateSelectedPage();

        // then the options are displayed
        Assert.NotNull(viewModel.SelectedPage);
        Assert.NotNull(viewModel.SelectedPageDetails);
        Assert.NotNull(viewModel.SelectedPageDetails.Page);
        Assert.Single(viewModel.SelectedPageDetails.Page.Options);
        Assert.Equal(["l"], viewModel.SelectedPageDetails.Page.Options.Select(o => o.Key).ToArray());
        Assert.Equal(["Luck test"], viewModel.SelectedPageDetails.Page.Options.Select(o => o.Label).ToArray());
        Assert.Equal(["TEST_STAT"], viewModel.SelectedPageDetails.Page.Options.Select(o => o.Command).ToArray());
        Assert.Equal(["stat"], viewModel.SelectedPageDetails.Page.Options.Where(o => o.Arguments != null).SelectMany(o => o.Arguments.Select(a => a.Key)).ToArray());
        Assert.Equal(["Luck"], viewModel.SelectedPageDetails.Page.Options.Where(o => o.Arguments != null).SelectMany(o => o.Arguments.Select(a => a.Value)).ToArray());
        
        var fightOption = viewModel.SelectedPageDetails.Page.Options.First();
        Assert.Equal(2, fightOption.Outcomes.Count());
        Outcome passOutcome = fightOption.Outcomes[0];
        Outcome failOutcome = fightOption.Outcomes[1];
        Assert.IsType<PassOutcome>(passOutcome);
        Assert.IsType<FailOutcome>(failOutcome);

        Assert.Equal("PASS", passOutcome.OutcomeType);

        Assert.NotNull(passOutcome.Story);
        Assert.NotNull(passOutcome.Story.Carets);
        Assert.Single(passOutcome.Story.Carets);
        Assert.Equal("text", passOutcome.Story.Carets.First().CaretType);
        Assert.Equal("\nyou escape without attracting\nthe Ogre's attention\n                            ", passOutcome.Story.Carets.First().StringValue);
        Assert.NotNull(passOutcome.Options);
        Assert.Single(passOutcome.Options);
        
        Assert.NotNull(passOutcome.Options.First());
        Assert.Equal("c", passOutcome.Options.First().Key);
        Assert.Equal("Continue", passOutcome.Options.First().Label);
        Assert.Equal("GOTO_GAME_PAGE", passOutcome.Options.First().Command);
        var passoption = passOutcome.Options.First();
        Assert.NotNull(passoption);
        Assert.NotNull(passoption.Arguments);
        Assert.Equal("page",passoption.Arguments.First().Key);
        Assert.Equal("1", passoption.Arguments.First().Value);
    }
}
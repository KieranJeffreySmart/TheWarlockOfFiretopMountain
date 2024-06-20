using bookeditor.ViewModels;

namespace bookeditor.test;

public class ViewOptions_Tests 
{

    [Theory]
    [InlineData("Start Command", 0, 1, new[] {"s"}, new[] {"Start"}, new[] {"START_GAME"})]
    [InlineData("Quit Command", 0, 1, new[] {"q"}, new[] {"Quit"}, new[] {"QUIT_GAME"})]
    [InlineData("Continue Command", 0, 1, new[] {"c"}, new[] {"Continue"}, new[] {"NEXT_PAGE"})]
    [InlineData("Continue and Back Commands", 0, 1, new[] {"c"}, new[] {"Continue"}, new[] {"NEXT_PAGE"})]
    [InlineData("Continue and Back Commands", 1, 1, new[] {"b"}, new[] {"Back"}, new[] {"PREVIOUS_PAGE"})]

    public void ViewSimplePageOptions(string testBook, int pageArrayIndex, int optionCount, string[] optionsKeys, string[] labels, string[] commands)
    {
        // Given I have a library
        var libraryName = "Books_With_Options";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
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
        Assert.NotNull(viewModel.SelectedPage);
        Assert.Equal(optionCount, viewModel.SelectedPage.Options.Length);
        Assert.Equal(optionsKeys, viewModel.SelectedPage.Options.Select(o => o.Key).ToArray());
        Assert.Equal(labels, viewModel.SelectedPage.Options.Select(o => o.Label).ToArray());
        Assert.Equal(commands, viewModel.SelectedPage.Options.Select(o => o.Command).ToArray());
    }
    

    [Theory]
    [InlineData("Goto Command", 0, 1, new[] {"g"}, new[] {"Go to Page 3"}, new[] {"GOTO_GAME_PAGE"}, new[] {"page"}, new[] {"3"})]
    [InlineData("Goto Command", 1, 2, new[] {"g", "g"}, new[] {"Go to Page 1", "Go to Page 3"}, new[] {"GOTO_GAME_PAGE", "GOTO_GAME_PAGE"}, new[] {"page", "page"}, new[] {"1", "3"})]
    [InlineData("Goto Continue and Back", 1, 3, new[] {"g", "c", "b"}, new[] {"Go to Page 1", "Continue", "Back"}, new[] {"GOTO_GAME_PAGE", "NEXT_PAGE", "PREVIOUS_PAGE"}, new[] {"page"}, new[] {"1"})]

    public void ViewPageOptionsWithArguments(string testBook, int pageArrayIndex, int optionCount, string[] optionsKeys, string[] labels, string[] commands, string[] argNames, string[] argValues)
    {
        // Given I have a library
        var libraryName = "Books_With_Options";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
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
        Assert.NotNull(viewModel.SelectedPage.Options);
        Assert.Equal(optionCount, viewModel.SelectedPage.Options.Length);
        Assert.Equal(optionsKeys, viewModel.SelectedPage.Options.Select(o => o.Key).ToArray());
        Assert.Equal(labels, viewModel.SelectedPage.Options.Select(o => o.Label).ToArray());
        Assert.Equal(commands, viewModel.SelectedPage.Options.Select(o => o.Command).ToArray());
        Assert.Equal(argNames, viewModel.SelectedPage.Options.Where(o => o.Arguments != null).SelectMany(o => o.Arguments.Select(a => a.Key)).ToArray());
        Assert.Equal(argValues, viewModel.SelectedPage.Options.Where(o => o.Arguments != null).SelectMany(o => o.Arguments.Select(a => a.Value)).ToArray());
    }



    [Fact]
    public void ViewStatCheckOptions()
    {
        // Given I have a library
        var libraryName = "Books_With_Options";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // When I open the book
        var viewModel = new BookEditorHomeViewModel(library, new EditorStateCache());
        Assert.NotNull(viewModel);
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
        Assert.NotNull(viewModel.SelectedPage.Options);

        var statCheckOpt = viewModel.SelectedPage.Options.First();
        Assert.Equal("l", statCheckOpt.Key);
        Assert.Equal("Luck test", statCheckOpt.Label);
        Assert.Equal("TEST_STAT", statCheckOpt.Command);

        Assert.NotNull(statCheckOpt.Arguments);
        Assert.Single(statCheckOpt.Arguments);
        var arg = statCheckOpt.Arguments.First();
        Assert.Equal("stat", arg.Key);
        Assert.Equal("Luck", arg.Value);
        
        Assert.NotNull(statCheckOpt.Outcomes);
        Assert.Equal(2, statCheckOpt.Outcomes.Count());

        var passOutcome = statCheckOpt.Outcomes[0];
        Assert.NotNull(passOutcome);

        Assert.Equal("PASS", passOutcome.OutcomeType);
        Assert.NotNull(passOutcome.Story);
        Assert.NotNull(passOutcome.Story.Carets);
        Assert.Single(passOutcome.Story.Carets);
        Assert.Equal("text", passOutcome.Story.Carets.First().CaretType);
        Assert.Equal("\nyou escape without attracting\nthe Ogre's attention\n                        ", passOutcome.Story.Carets.First().StringValue);
        Assert.NotNull(passOutcome.Options);
        Assert.Single(passOutcome.Options);
        
        Assert.NotNull(passOutcome.Options.First());
        Assert.Equal("c", passOutcome.Options.First().Key);
        Assert.Equal("Continue", passOutcome.Options.First().Label);
        Assert.Equal("GOTO_GAME_PAGE", passOutcome.Options.First().Command);
        
        var passgotooption = passOutcome.Options.First();
        Assert.NotNull(passgotooption);
        Assert.NotNull(passgotooption.Arguments);
        Assert.Equal("page",passgotooption.Arguments.First().Key);
        Assert.Equal("1", passgotooption.Arguments.First().Value);
        
        var failOutcome = statCheckOpt.Outcomes[1];;
        Assert.NotNull(failOutcome);

        Assert.Equal("FAIL", failOutcome.OutcomeType);
        Assert.NotNull(failOutcome.Story);
        Assert.NotNull(failOutcome.Story.Carets);
        Assert.Single(failOutcome.Story.Carets);
        Assert.Equal("text", failOutcome.Story.Carets.First().CaretType);

        Assert.Equal("\nyou curse as you kick a small stone which goes\nskidding across the cavern floor. You draw your\nsword in case the Ogre has heard it\n                        ", failOutcome.Story.Carets.First().StringValue);
        Assert.NotNull(failOutcome.Options);
        Assert.Single(failOutcome.Options);
        
        Assert.NotNull(failOutcome.Options.First());
        Assert.Equal("c", failOutcome.Options.First().Key);
        Assert.Equal("Continue", failOutcome.Options.First().Label);
        Assert.Equal("GOTO_GAME_PAGE", failOutcome.Options.First().Command);
        
        var failGotoOption = failOutcome.Options.First();
        Assert.NotNull(failGotoOption);
        Assert.NotNull(failGotoOption.Arguments);
        Assert.Equal("page",failGotoOption.Arguments.First().Key);
        Assert.Equal("2", failGotoOption.Arguments.First().Value);
    }
}
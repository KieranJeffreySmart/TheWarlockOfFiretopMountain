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
        Assert.NotNull(viewModel.SelectedPageDetails.Page.Options);
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
        Assert.NotNull(viewModel.SelectedPageDetails.Page.Options);
        Assert.Single(viewModel.SelectedPageDetails.Page.Options);
        Assert.NotNull(viewModel.SelectedPageDetails.OptionViewModels);
        Assert.Single(viewModel.SelectedPageDetails.OptionViewModels);
        var statCheckvm = viewModel.SelectedPageDetails.OptionViewModels.First();
        Assert.NotNull(statCheckvm.Option);

        var statCheckOpt = statCheckvm.Option;
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
        Assert.NotNull(statCheckvm.OutcomeViewModels);
        Assert.NotEmpty(statCheckvm.OutcomeViewModels);
        Assert.Equal(2, statCheckvm.OutcomeViewModels.Count());

        var passOutcomevm = statCheckvm.OutcomeViewModels[0];
        Assert.NotNull(passOutcomevm);
        var passOutcome = passOutcomevm.Outcome;
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

        Assert.NotNull(passOutcomevm.OptionViewModels);
        Assert.NotNull(passOutcomevm.OptionViewModels.First());
        var passgotooptionvm = passOutcomevm.OptionViewModels.First();
        
        var passgotooption = passgotooptionvm.Option;
        Assert.NotNull(passgotooption);
        Assert.NotNull(passgotooption.Arguments);
        Assert.Equal("page",passgotooption.Arguments.First().Key);
        Assert.Equal("1", passgotooption.Arguments.First().Value);
        
        var failOutcomevm = statCheckvm.OutcomeViewModels[1];        
        Assert.NotNull(failOutcomevm);
        var failOutcome = failOutcomevm.Outcome;
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

        Assert.NotNull(failOutcomevm.OptionViewModels);
        Assert.NotNull(failOutcomevm.OptionViewModels.First());
        var failGotoOptionvm = failOutcomevm.OptionViewModels.First();
        
        var failGotoOption = failGotoOptionvm.Option;
        Assert.NotNull(failGotoOption);
        Assert.NotNull(failGotoOption.Arguments);
        Assert.Equal("page",failGotoOption.Arguments.First().Key);
        Assert.Equal("2", failGotoOption.Arguments.First().Value);
    }
}
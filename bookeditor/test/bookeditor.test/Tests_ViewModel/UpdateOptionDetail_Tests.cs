using bookeditor.ViewModels;
using Arrange = bookeditor.test.ViewModelArrangement;

namespace bookeditor.test;

public class UpdateOptionDetail_Tests
{
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/ChangeOptionKey.xml", "../../../TestData/Books_With_Options.xml")]
    public void ChangeOptionKey() 
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "ChangeOptionKey";
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

        homePage.SelectOption(option1);
        Assert.NotNull(homePage.SelectedOption);

        // when I change the key
        homePage.SelectedOption.Key = "x";
        homePage.ApplyChanges();

        // the new key is displayed 
        
    }
}
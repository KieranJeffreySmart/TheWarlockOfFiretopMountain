using System.Xml;
using System.Xml.Linq;
using bookeditor.ViewModels;

namespace bookeditor.test;

public class SaveBook_Tests
{
    [Fact]
    public async Task SaveModifiedBook()
    {
        // given I have a library
        var rootPath = "../../../TestData";
        var defaultLibrary = "Save_Modified_Book";
        var library = new XmlLibrary(rootPath, [defaultLibrary]);
        library.DefaultLibraryName = defaultLibrary;
        var fullPath = Path.Combine(rootPath, $"{defaultLibrary}.xml");
        
        // given I have a book with a title
        var title = "Test saving a modified book";
        var slug = "test-slug";

        using (XmlWriter writer = XmlWriter.Create(fullPath, new XmlWriterSettings { Async = true }))
        {
            await XDocument.Parse($"<library><book><slug>{slug}</slug><title>{title}</title><page><type>Intro</type><index>1</index><story><caret type=\"text\">modify story original value</caret></story></page></book></library>")
                .SaveAsync(writer, CancellationToken.None);
        }
        
        var cache = new EditorStateCache();

        // given I have a home page 
        BookEditorHomeViewModel homePage = new(library, cache);
        Assert.NotNull(homePage);

        // given I have a book
        Assert.NotNull(homePage.Books);
        var book = homePage.Books.First(b => b.Title == title);
        homePage.SelectedBook = book;

        // given I have a page with some story
        Assert.NotNull(book);
        Assert.NotNull(book.Pages);
        Assert.Single(book.Pages);
        var page = book.Pages.First();
        homePage.SelectedPage = page;
        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Carets);
        Assert.Single(page.Story.Carets);
        var caret = page.Story.Carets.First();
        Assert.Equal("modify story original value", caret.StringValue);

        // when I change the story
        caret.StringValue = "new test value";

        // when I save to file
        await homePage.SaveToFile();

        // when I re-open the app
        library = new XmlLibrary("../../../TestData", [library.DefaultLibraryName]);
        cache = new EditorStateCache();
        homePage = new(library, cache);
        Assert.NotNull(homePage);
        Assert.NotNull(homePage.Books);
        book = homePage.Books.First(b => b.Title == title);


        // then the changes should be displayed
        Assert.NotNull(book);
        Assert.NotNull(book.Pages);
        Assert.Single(book.Pages);
        page = book.Pages.First();
        Assert.NotNull(page.Story);
        Assert.NotNull(page.Story.Carets);
        Assert.Single(page.Story.Carets);
        caret = page.Story.Carets.First();
        Assert.Equal("new test value", caret.StringValue);

        
        if (File.Exists(fullPath)) 
        {
            File.Delete(fullPath);
        }
    }
}
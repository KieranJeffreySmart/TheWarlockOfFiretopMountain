using System.Xml;
using System.Xml.Linq;

namespace bookeditor.test;

public class XmlLibrary_WriteIntegrationTests
{
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/Test_New_Library.xml", skipCreate: true)]
    public async Task SaveNewBookToNewLibraryFile()
    {    
        // given I have a default library name and path
        var rootPath = "../../../TestData";
        var library = new XmlLibrary(rootPath, []);
        var defaultLibrary = "Test_New_Library";
        library.DefaultLibraryName = defaultLibrary;

        // given I have a book with a title
        var title = "Test New Library";
        var book = new Book { Title = title};
        book.Pages = [
            new Page {
                PageType="Intro",
                Index=1,
                Story = new Story { Carets = [new Caret { CaretType = "text", StringValue = "test page 1"}]}
            },
            new Page {
                PageType="Intro",
                Index=2,
                Story = new Story { Carets = [new Caret { CaretType = "text", StringValue = "test page 2"}]}
            },
            new Page {
                PageType="Intro",
                Index=3,
                Story = new Story { Carets = [new Caret { CaretType = "text", StringValue = "test page 3"}]}
            }
        ];

        // when I write it to the library
        await library.WriteBookToLibrary(book);

        // when I open the default library
        library = new XmlLibrary("../../../TestData", [defaultLibrary]);

        // then the new book should exist
        Assert.NotNull(library.Books);
        Assert.Single(library.Books);
        Assert.Equal(title, library.Books.First().Title);
        
        Assert.True(Guid.TryParse(library.Books.First().Slug, out var slug));  

        foreach (var page in library.Books.First().Pages)
        {
            Assert.True(Guid.TryParse(page.Slug, out var pag_slug));  
        }      
    }
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/Write_To_Empty_File.xml")]
    public async Task SaveBookToEmptyFile()
    {    
        // given I have an empty library file
        var rootPath = "../../../TestData";
        var library = new XmlLibrary(rootPath, []);
        var defaultLibrary = "Write_To_Empty_File";
        library.DefaultLibraryName = defaultLibrary;

        // given I have a book with a title
        var title = "Test New Library";
        var book = new Book { Title = title};

        // when I write it to the library
        await library.WriteBookToLibrary(book);

        // when I open the default library
        library = new XmlLibrary("../../../TestData", [defaultLibrary]);

        // then the new book should exist
        Assert.NotNull(library.Books);
        Assert.Single(library.Books);
        Assert.Equal(title, library.Books.First().Title);
        
        Assert.True(Guid.TryParse(library.Books.First().Slug, out var slug));
    }

    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/Write_To_Existing_File.xml", "../../../TestData/Empty_Library.xml")]
    public async Task SaveBookToExistingLibrary()
    {    
        // given I have a new library
        var rootPath = "../../../TestData";
        var library = new XmlLibrary(rootPath, []);
        var defaultLibrary = "Write_To_Existing_File";
        library.DefaultLibraryName = defaultLibrary;

        // given I have a book with a title
        var title = "Test New Library";
        var slug = "test slug";
        var book = new Book { Slug = slug, Title = title};

        // when I write it to the library
        await library.WriteBookToLibrary(book);

        // when I open the default library
        library = new XmlLibrary("../../../TestData", [defaultLibrary]);

        // then the new book should exist
        Assert.NotNull(library.Books);
        Assert.Single(library.Books);
        Assert.Equal(title, library.Books.First().Title);
        Assert.Equal(slug, library.Books.First().Slug);   
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/Replace_In_Existing_File.xml", "../../../TestData/Books_With_Slugs.xml")]
    public async Task SaveExistingBookToExistingLibrary()
    {      
        // given I have a library
        var rootPath = "../../../TestData";
        var library = new XmlLibrary(rootPath, []);
        var defaultLibrary = "Replace_In_Existing_File";
        library.DefaultLibraryName = defaultLibrary;
        
        // given I have a book with a title
        var bookSlug = "test-book-slug";

        // given I have a book with the same slug but different title
        var newTitle = "It works Yay!!!";
        var book = new Book { Slug = bookSlug, Title = newTitle};

        // when I write it to the library
        await library.WriteBookToLibrary(book);

        // when I open the default library
        library = new XmlLibrary("../../../TestData", [defaultLibrary]);

        // then the same book should exist with a new title
        Assert.NotNull(library.Books);
        Assert.Single(library.Books);
        var savedBook = library.Books.FirstOrDefault(b => b.Slug == bookSlug);
        Assert.NotNull(savedBook);
        Assert.Equal(newTitle, savedBook.Title);
    }
}

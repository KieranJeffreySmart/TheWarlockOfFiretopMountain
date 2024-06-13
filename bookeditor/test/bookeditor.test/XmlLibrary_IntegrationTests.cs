using System.Xml;

namespace bookeditor.test;

public class XmlLibrary_IntegrationTests
{
    [Fact]
    public void GetBookFromEmptyFile()
    {        
        // given I have a library on file
        var libraryName = "Empty_File";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // given I have a book title that does not exist
        var booktitle = "Any book";

        // when I get the book
        Book book = library.GetBook(booktitle);

        // [rgR] should I receive a book at all?
        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("", book.Title);
    }

    [Fact]
    public void GetMissingBookFromFile()
    {
        // given I have a library on file
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // given I have a book title that does not exist
        var booktitle = "Missing book";

        // when I get the book
        Book book = library.GetBook(booktitle);

        // [rgR] should I receive a book at all?
        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("", book.Title);
    }
    
    [Fact]
    public void GetBookFromFile()
    {
        // given I have a library on file
        var libraryName = "Warlock_of_Firetop_Mountain";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // given I have a book title
        var booktitle = "Warlock of Firetop Mountain";

        // when I get the book
        Book book = library.GetBook(booktitle);

        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("Warlock of Firetop Mountain", book.Title);
    }

    [Fact]
    public void GetBookFromNewRootPath()
    {
        // given I have a library on file in a new folder
        var libraryName = "Warlock_of_Firetop_Mountain";
        var library = new XmlLibrary("", [libraryName]);

        // given I have a book title
        var booktitle = "Warlock of Firetop Mountain";

        // when I get the book
        Book blank_book = library.GetBook(booktitle);

        // then I should receive a blank book:
        Assert.NotNull(blank_book);
        Assert.Equal("", blank_book.Title);

        // given I have a new root path
        string rootPath = "../../../TestData";
        
        // when I set the root path
        library.RootPath = rootPath;

        // when I get the book
        Book book = library.GetBook(booktitle);

        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("Warlock of Firetop Mountain", book.Title);
    }
    
    [Fact]
    public void GetBookFirstFromFile()
    {
        // given I have a library on file
        var libraryName = "Books_With_Pages";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // when I get the first book
        Book book = library.GetFirstBook();

        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("Empty book", book.Title);
    }
    
    [Fact]
    public void GetBookFirstFromEmptyLibraryFile()
    {
        // given I have a library on file
        var libraryName = "Empty_Library";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // when I get the first book
        Book book = library.GetFirstBook();

        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("", book.Title);
    }
    
    
    [Fact]
    public async Task GetBooksFromManyFiles()
    {
        // given I have a library on file
        string[] libraryNames = ["Warlock_of_Firetop_Mountain", "Books_With_Pages", "Empty_Library"];
        var library = new XmlLibrary("../../../TestData", libraryNames);

        // when I get all books
        var asyncbooks = library.GetAllBooks();

        // then I should receive all available books:
        Assert.NotNull(asyncbooks);
        List<Book> books = new List<Book>();
        await foreach (var book in asyncbooks)
        {
            books.Add(book);
        }

        Assert.NotEmpty(books);
        Assert.Equal(8, books.Count);
        Assert.NotNull(books.FirstOrDefault(b => b.Title == "Warlock of Firetop Mountain"));
        Assert.NotNull(books.FirstOrDefault(b => b.Title == "Empty book"));
        Assert.NotNull(books.FirstOrDefault(b => b.Title == "Single Intro book"));
        Assert.NotNull(books.FirstOrDefault(b => b.Title == "Single Game book"));
        Assert.NotNull(books.FirstOrDefault(b => b.Title == "Single Game and Intro book"));
        Assert.NotNull(books.FirstOrDefault(b => b.Title == "Many Intro book"));
        Assert.NotNull(books.FirstOrDefault(b => b.Title == "Many Game book"));
        Assert.NotNull(books.FirstOrDefault(b => b.Title == "Many Game and Intro book"));
    }
        
    [Fact]
    public async Task CanDeleteFileAfterOpening()
    {
        // given I have a new library on file
        var libraryName = "Can_Delete_Library";
        var path = "../../../TestData";
        var completeFilePath = Path.Combine(path, $"{libraryName}.xml");
        
        XmlDocument xdoc = new XmlDocument();
        xdoc.LoadXml("<library></library>");

        using (var writer = XmlWriter.Create(completeFilePath))
        {
            xdoc.WriteContentTo(writer);
        }

        var library = new XmlLibrary(path, [libraryName]);

        // when I get all books
        var asyncbooks = library.GetAllBooks();
        Assert.NotNull(asyncbooks);
        List<Book> books = new List<Book>();
        await foreach (var book in asyncbooks)
        {
            books.Add(book);
        }

        // then I should be able to delete the file
        File.Delete(completeFilePath);
        Assert.False(File.Exists(completeFilePath));
    }

    
        
    [Fact]
    public async Task OpenAllFiles()
    {
        // given I have a folder with many libraries on file
        var libraryName = "*";
        var path = "../../../TestData/OpenAllFiles";
        var library = new XmlLibrary(path, [libraryName]);

        // when I get all books
        var asyncbooks = library.GetAllBooks();
        Assert.NotNull(asyncbooks);
        List<Book> books = new List<Book>();
        await foreach (var book in asyncbooks)
        {
            books.Add(book);
        }

        // then I should be receive the expected number of books
        Assert.Equal(17, books.Count);        
    }
}

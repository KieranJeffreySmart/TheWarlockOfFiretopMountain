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
}

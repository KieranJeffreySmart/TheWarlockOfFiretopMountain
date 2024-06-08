namespace bookeditor.test;

public class XmlBookRepo_IntegrationTests
{
    [Fact]
    public void GetBookFromEmptyFile()
    {        
        // given I have a library on file
        var library = "Empty_File";
        var testDataRepository = new XmlBookRepo("../../../TestData");

        // given I have a book name that does not exist
        var bookname = "Any book";

        // when I get the book
        Book book = testDataRepository.GetBook(library, bookname);

        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("", book.Name);
    }

    [Fact]
    public void GetMissingBookFromFile()
    {
        // given I have a library on file
        var library = "Books_With_Pages";
        var testDataRepository = new XmlBookRepo("../../../TestData");

        // given I have a book name that does not exist
        var bookname = "Missing book";

        // when I get the book
        Book book = testDataRepository.GetBook(library, bookname);

        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("", book.Name);
    }
    
    [Fact]
    public void GetBookFromFile()
    {
        // given I have a library on file
        var library = "Books_With_Pages";
        var testDataRepository = new XmlBookRepo("../../../TestData");

        // given I have a book name
        var bookname = "Empty book";

        // when I get the book
        Book book = testDataRepository.GetBook(library, bookname);

        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("Empty book", book.Name);
    }
}

namespace bookeditor.test;

public class XmlBookRepo_IntegrationTests
{
    [Fact]
    public void GetBookFromEmptyFile()
    {
    }

    [Fact]
    public void GetBookFromMissingFile()
    {
    }
    
    [Fact]
    public void GetBookFromFile()
    {
        // given I have a library on file
        var library = "Books_With_Pages";
        var testDataRepository = new XmlBookRepo("../../../TestData");

        // given I have a book name
        var bookname = "Empty Book";

        // when I get the book
        Book book = testDataRepository.GetBook(library, bookname);

        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("Empty Book", book.Name);
    }
}

using System.Xml;

namespace bookeditor.test;

public class XmlLibrary_ReadIntegrationTests
{
    [Fact]
    public void GetBooksFromEmptyFile()
    {        
        // given I have a library on file
        var libraryName = "Empty_File";
        var library = new XmlLibrary("../../../TestData", [libraryName]);

        // when I get books
        Assert.NotNull(library.Books);
        var books = library.Books.ToArray();

        // [rgR] should I receive a book at all?
        // then I should receive a book with:
        Assert.NotNull(library.Books);
        Assert.Empty(library.Books);
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
        Assert.NotNull(library.Books);
        Book book = library.Books.First(b => b.Title == booktitle);

        // then I should receive a book with:
        Assert.NotNull(book);
        Assert.Equal("Warlock of Firetop Mountain", book.Title);
    }

    [Fact]
    public void GetBookFromNewRootPath()
    {
        // given I have a library on file in a new folder
        var libraryName = "Warlock_of_Firetop_Mountain";
        var library = new XmlLibrary("../../../", [libraryName]);


        // when I get books
        Assert.NotNull(library.Books);

        // then I should not find any:
        Assert.Empty(library.Books);

        // given I have a new root path
        string rootPath = "../../../TestData";
        
        // when I set the root path
        library.RootPath = rootPath;

        // when I get the book
        Assert.NotNull(library.Books);

        // then I should receive a book with:
        Assert.NotNull(library.Books);
        Assert.Single(library.Books);
        Assert.Equal("Warlock of Firetop Mountain", library.Books.First().Title);
    }
    
    
    [Fact]
    public void GetBooksFromManyFiles()
    {
        // given I have a library on file
        string[] libraryNames = ["Warlock_of_Firetop_Mountain", "Books_With_Pages", "Empty_Library"];
        var library = new XmlLibrary("../../../TestData/OpenAllFiles", libraryNames);

        // when I get all books
        Assert.NotNull(library.Books);
        Assert.NotEmpty(library.Books);

        // then I should receive all available books:
        Assert.NotNull(library.Books.FirstOrDefault(b => b.Title == "Warlock of Firetop Mountain"));
        Assert.NotNull(library.Books.FirstOrDefault(b => b.Title == "Empty book"));
        Assert.NotNull(library.Books.FirstOrDefault(b => b.Title == "Single Intro book"));
        Assert.NotNull(library.Books.FirstOrDefault(b => b.Title == "Single Game book"));
        Assert.NotNull(library.Books.FirstOrDefault(b => b.Title == "Single Game and Intro book"));
        Assert.NotNull(library.Books.FirstOrDefault(b => b.Title == "Many Intro book"));
        Assert.NotNull(library.Books.FirstOrDefault(b => b.Title == "Many Game book"));
        Assert.NotNull(library.Books.FirstOrDefault(b => b.Title == "Many Game and Intro book"));

        Assert.Equal(8, library.Books.Length);
    }
        
    [Fact]
    public void CanDeleteFileAfterOpening()
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
        Assert.NotNull(library.Books);

        // then I should be able to delete the file
        File.Delete(completeFilePath);
        Assert.False(File.Exists(completeFilePath));
    }

    
        
    [Fact]
    public void OpenAllFiles()
    {
        // given I have a folder with many libraries on file
        var libraryName = "*";
        var path = "../../../TestData/OpenAllFiles";
        var library = new XmlLibrary(path, [libraryName]);

        // when I get all books
        Assert.NotNull(library.Books);
        // then I should be receive the expected number of books
        Assert.Equal(18, library.Books.Length);        
    }

    
    [Fact]
    public void GetAllBooksFromAllFiles()
    {
        // given I have a folder with many libraries on file
        var libraryName = "*";
        var path = "../../../TestData/OpenAllFiles";
        var library = new XmlLibrary(path, [libraryName]);

        // when I get all books
        var books = library.Books;
        Assert.NotNull(books);
        // then I should be receive the expected number of books
        Assert.Equal(18, books.Length);
    }
}

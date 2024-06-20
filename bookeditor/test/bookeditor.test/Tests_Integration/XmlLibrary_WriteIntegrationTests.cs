using System.Xml;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace bookeditor.test;

public class XmlLibrary_WriteIntegrationTests
{
    [Fact]
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

        // when I write it to the library
        await library.WriteBookToLibrary(book);

        // when I open the default library
        library = new XmlLibrary("../../../TestData", [defaultLibrary]);

        // then the new book should exist
        Assert.NotNull(library.Books);
        Assert.Single(library.Books);
        Assert.Equal(title, library.Books.First().Title);
        
        Assert.True(Guid.TryParse(library.Books.First().Slug, out var slug));        

        if (File.Exists(Path.Combine(rootPath, $"{defaultLibrary}.xml"))) 
        {
            File.Delete(Path.Combine(rootPath, $"{defaultLibrary}.xml"));
        }
    }    
    
    [Fact]
    public async Task SaveBookToEmptyFile()
    {    
        // given I have an empty library file
        var rootPath = "../../../TestData";
        var library = new XmlLibrary(rootPath, []);
        var defaultLibrary = "Write_To_Empty_File";
        library.DefaultLibraryName = defaultLibrary;
        var fullPath = Path.Combine(rootPath, $"{defaultLibrary}.xml");

        File.Create(fullPath).Dispose();

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

        if (File.Exists(fullPath)) 
        {
            File.Delete(fullPath);
        }
    }

    
    [Fact]
    public async Task SaveBookToExistingLibrary()
    {    
        // given I have a new library
        var rootPath = "../../../TestData";
        var library = new XmlLibrary(rootPath, []);
        var defaultLibrary = "Write_To_Existing_File";
        library.DefaultLibraryName = defaultLibrary;
        var fullPath = Path.Combine(rootPath, $"{defaultLibrary}.xml");

        using (XmlWriter writer = XmlWriter.Create(fullPath, new XmlWriterSettings { Async = true }))
        {
            await XDocument.Parse("<library></library>").SaveAsync(writer, CancellationToken.None);
        }

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

        if (File.Exists(fullPath)) 
        {
            File.Delete(fullPath);
        }
    }

    [Fact]
    public async Task SaveExistingBookToExistingLibrary()
    {      
        // given I have a library
        var rootPath = "../../../TestData";
        var library = new XmlLibrary(rootPath, []);
        var defaultLibrary = "Replace_In_Existing_File";
        library.DefaultLibraryName = defaultLibrary;
        var fullPath = Path.Combine(rootPath, $"{defaultLibrary}.xml");
        
        // given I have a book with a title
        var title = "Test saving an xisting book in Existing Library";
        var slug = "test-slug";

        using (XmlWriter writer = XmlWriter.Create(fullPath, new XmlWriterSettings { Async = true }))
        {
            await XDocument.Parse($"<library><book><slug>{slug}</slug><title>{title}</title></book></library>").SaveAsync(writer, CancellationToken.None);
        }

        // given I have a book with the same slug but different title
        var newTitle = "It works Yay!!!";
        var book = new Book { Slug = slug, Title = newTitle};

        // when I write it to the library
        await library.WriteBookToLibrary(book);

        // when I open the default library
        library = new XmlLibrary("../../../TestData", [defaultLibrary]);

        // then the same book should exist with a new title
        Assert.NotNull(library.Books);
        Assert.Single(library.Books);
        Assert.Equal(newTitle, library.Books.First().Title);
        Assert.Equal(slug, library.Books.First().Slug);    

        if (File.Exists(fullPath)) 
        {
            File.Delete(fullPath);
        }
    }
}
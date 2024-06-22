namespace bookeditor.test;

public class OptionList_Tests
{
    [Fact]
    public void AppendAStartOptionToPageWithNoOptions()
    {
        // given I have a library
        // given I have opened a book with a page with no options
        // when I append an option
        // then the page is displayed with only the new option

    }

    [Fact]
    public void AppendQuitOptionToPageWithSingleOption()
    {
        // given I have a library
        // given I have opened a book with a page with a single option
        // when I append an option
        // then the page is displayed with both options in order
    }

    [Fact]
    public void AppendQuitOptionToPageWithManyOptions()
    {
        // given I have a library
        // given I have opened a book with a page with many options
        // when I append an option
        // then the page is displayed with all options in order
    }

    [Fact]
    public void InsertContinueOptionToPageWithNoOptions()
    {
        // given I have a library
        // given I have opened a book with a page with many options
        // when I insert an option
        // then the page is displayed with only the new option
    }

    [Fact]
    public void InsertContinueOptionToPageWithSingleOption()
    {
        // given I have a library
        // given I have opened a book with a page with a single option
        // when I insert an option after the first
        // then the page is displayed with both options in order

    }

    [Fact]
    public void InsertContinueOptionToPageWithManyOptions()
    {
        // given I have a library
        // given I have opened a book with a page with many options
        // when I insert an option after the first
        // then the page is displayed with all options in order
    }

    [Fact]
    public void InsertContinueOptionToPageWithIndexOutOfBounds()
    {
        // given I have a library
        // given I have opened a book with a page with many options
        // when I insert an option after a position that is not in the list
        // then the page is displayed with both options in order
    }
    
    [Fact]
    public void RemoveOptionFromPageWithSingleOption()
    {
        // given I have a library
        // given I have opened a book with a page with a single option
        // when I remove an option
        // then the page is displayed with no options
    }
    
    [Fact]
    public void RemoveOptionFromPageWithManyOptions()
    {
        // given I have a library
        // given I have opened a book with a page with many options
        // when I remove the first option
        // then the page is displayed with only options after the first
    }

    [Fact]
    public void RemoveOptionFromPageWithIndexOutOfBounds()
    {
        // given I have a library
        // given I have opened a book with a page with many options
        // when I remove an option
        // then I am informed of an error

    }

}
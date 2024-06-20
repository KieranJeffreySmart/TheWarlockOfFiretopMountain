using bookeditor.ViewModels;

namespace bookeditor.test;

public class CaretList_Tests
{
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendToEmptyStoryCaret.xml", "../../../TestData/Books_With_Slugs.xml")]
    public void AppendToEmptyStoryCaret()
    {
        // given I have a book with no story
        // when I append a caret to the story
        // then that story is displayed with only the new caret
    }

    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/AppendStoryCaret.xml", "../../../TestData/Books_With_Slugs.xml")]
    public void AppendStoryCaret()
    {
        // given I have a book with a story that has a caret
        // when I append a caret to the story
        // then that story is displayed with the two carets in order
    }
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/InsertStoryCaret.xml", "../../../TestData/Books_With_Slugs.xml")]
    public void InsertStoryCaret()
    {
        // given I have a book with a story that has two carets
        // when I insert a caret after the first
        // then that story is displayed with the three carets in order
    }
    
    [Fact]
    [CreateRemoveFileBeforeAfter("../../../TestData/RemoveStoryCaret.xml", "../../../TestData/Books_With_Slugs.xml")]
    public void RemoveStoryCaret()
    {
        // given I have a book with a story that has two caret
        // when I delete the second caret
        // then that story is displayed with only the first caret
    }
}
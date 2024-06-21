using System.Text;

namespace bookeditor.test;

public class CreateRemoveFileBeforeAfter_UnitTests
{
    [Fact]
    public void CreateAndRemoveAFile()
    {
        var testFilePath = "../../../TestData/TestCreateRemove.txt";
        var attribute = new CreateRemoveFileBeforeAfter(testFilePath);
        Assert.False(File.Exists(testFilePath));

        attribute.Before(null);
        
        Assert.True(File.Exists(testFilePath));
        
        attribute.After(null);
        Assert.False(File.Exists(testFilePath));
    }

    [Fact]
    public void OnlyRemoveAFile()
    {
        var testFilePath = "../../../TestData/TestOnlyRemove.txt";
        var attribute = new CreateRemoveFileBeforeAfter(testFilePath, skipCreate: true);
        Assert.False(File.Exists(testFilePath));
        File.Create(testFilePath).Dispose();

        attribute.Before(null);

        Assert.False(File.Exists(testFilePath));
        File.Create(testFilePath).Dispose();
        
        Assert.True(File.Exists(testFilePath));
        
        attribute.After(null);
        Assert.False(File.Exists(testFilePath));
    }

    [Fact]
    public void CreateCopyAndRemoveAFile()
    {
        var testFilePath = "../../../TestData/TestCreateCopyRemove.txt";
        var sourceFilePath = "../../../TestData/File_WithContent.txt";
        var sourceContent = "some content";
        var attribute = new CreateRemoveFileBeforeAfter(testFilePath, sourceFilePath);
        Assert.False(File.Exists(testFilePath));
        Assert.False(File.Exists(sourceFilePath));

        var sourceFileStream = File.Create(sourceFilePath);
        sourceFileStream.Write(Encoding.UTF8.GetBytes(sourceContent));
        sourceFileStream.Dispose();

        attribute.Before(null);
        
        Assert.True(File.Exists(testFilePath));
        var content = File.ReadAllText(testFilePath);
        Assert.Equal(sourceContent, content);
        attribute.After(null);
        Assert.False(File.Exists(testFilePath));

        File.Delete(sourceFilePath);
    }
}
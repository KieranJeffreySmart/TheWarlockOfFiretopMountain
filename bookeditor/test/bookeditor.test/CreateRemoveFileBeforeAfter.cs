using System.Reflection;
using Xunit.Sdk;

namespace bookeditor.test;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class CreateRemoveFileBeforeAfter : BeforeAfterTestAttribute
{
    private readonly string filePath;

    private readonly string? copyFromPath;

    private readonly bool skipCreate;

    public CreateRemoveFileBeforeAfter(string filePath, string? copyFromPath = null, bool skipCreate = false)
    {
        this.filePath = Path.GetFullPath(filePath);

        if (!string.IsNullOrEmpty(copyFromPath))
        {
            this.copyFromPath = Path.GetFullPath(copyFromPath);
        }

        this.skipCreate = skipCreate;
    }

    public override void Before(MethodInfo methodUnderTest)
    {
        if (File.Exists(this.filePath))
        {
            File.Delete(this.filePath);
        }
        
        if (skipCreate) return;

        if (!string.IsNullOrEmpty(this.copyFromPath) && File.Exists(this.copyFromPath))
        {
            File.Copy(this.copyFromPath, this.filePath);
            return;
        }
        

        File.Create(this.filePath).Dispose();
    }

    public override void After(MethodInfo methodUnderTest)
    {
        if (File.Exists(this.filePath))
        {
            File.Delete(this.filePath);
        }
    }
}
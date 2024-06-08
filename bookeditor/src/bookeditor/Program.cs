using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace bookeditor;
public class Program
{
    public static void Main(string[] args)
    {
        var host = WebHost.CreateDefaultBuilder(args)
            .UseIISIntegration()
            .UseStartup<Startup>()
            .Build();

        host.Run();
    }
}

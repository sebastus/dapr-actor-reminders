using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Dapr.Actors.AspNetCore;
using Microsoft.AspNetCore;

namespace MyActorService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseActors(options =>
            {
                options.Actors.RegisterActor<MyActor>();
            });
    }
}

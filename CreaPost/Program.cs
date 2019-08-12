using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CreaPost.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CreaPost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var host = BuildWebHost(args);

            SeedDb(host);

            host.Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();

        public static void SeedDb(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<CreaPostSeeder>();
                seeder.SeedAsync().Wait();
            }

        }

            public static IWebHost BuildWebHost(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                        .ConfigureAppConfiguration(SetupConfiguration)
                        .UseStartup<Startup>()
                        .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration))
                        .Build();

        public static void SetupConfiguration(WebHostBuilderContext context, IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Sources.Clear();

            configurationBuilder.AddJsonFile("appsettings.json", false, true)
                                .AddEnvironmentVariables();
        }
    }
}

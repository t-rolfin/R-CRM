using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crm.infrastructure.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using crm.infrastructure.Extensions;
using crm.infrastructure.Identity;
using System.IO;
using crm.infrastructure.Seeding;

namespace crm.api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args)
                .ConfigureAppConfiguration(configs => GetConfigurations())
                .Build();

            host.MigrateDbContext<IdentityContext>((context, serviceProvider) =>
            {
                Seeding.SeedPermissions(context);
                context.SaveChanges();
            });

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, serviceProvider, loggerConfigs)
                        => SerilogConfigs.GetConfigs(context, serviceProvider, loggerConfigs))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder()
            .CaptureStartupErrors(false)
            .UseStartup<Startup>()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureLogging(x => x.AddSerilog());

        public static IConfiguration GetConfigurations()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}

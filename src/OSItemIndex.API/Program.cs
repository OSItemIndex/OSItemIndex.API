using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OSItemIndex.Data.Database;
using Serilog.Exceptions;

namespace OSItemIndex.API
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                         .Enrich.FromLogContext()
                         .Enrich.WithExceptionDetails()
                         .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                         .CreateBootstrapLogger();

            var webHost = CreateWebHost(args).Build();
            using (var scope = webHost.Services.CreateScope()) // InitializeDb
            {
                var serviceProvider = scope.ServiceProvider;
                var dbInitializer = serviceProvider.GetRequiredService<IDbInitializerService>();
                await dbInitializer.InitializeDatabaseAsync(serviceProvider);
            }
            await webHost.RunAsync();
            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateWebHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>()
                       .UseUrls("http://*:5000")
                       .UseSerilog((context, configuration) =>
                {
                    configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                 .Enrich.FromLogContext()
                                 .Enrich.WithExceptionDetails()
                                 .WriteTo
                                 .Console(outputTemplate:
                                          "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"); // {Properties:j}
                });
            });
        }
    }
}

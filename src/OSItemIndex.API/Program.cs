using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
            await webHost.RunAsync();
            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateWebHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>()
                       .UseSerilog((context, configuration) =>
                       {
                           configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                        .Enrich.FromLogContext()
                                        .Enrich.WithExceptionDetails()
                                        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"); // {Properties:j}
                       });
            });
        }
    }
}

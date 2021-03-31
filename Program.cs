using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OSItemIndex.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildHost(args).Run();
            Log.CloseAndFlush();
        }

        public static IHost BuildHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)

                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();

                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}") // {Properties:j}
                        .CreateLogger();

                    builder.AddSerilog(Log.Logger, true);
                })

                .UseSerilog()

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })

                .Build();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text.Json;

namespace OSItemIndex.API.HealthChecks
{
    public static class HealthCheckServicesConfiguration
    {
        public static IServiceCollection AddHealthCheckServices(this IServiceCollection services)
        {
            services.AddHealthChecks()
                //.AddDbContextCheck<OSItemIndexDbContext>()
                .AddCheck<ItemsIntegrityHealthCheck>("ItemsIntegrityHealthCheck")
                .AddCheck<DbContextCheck>("DbContextCheck");
            return services;
        }

        public static IApplicationBuilder UseHealthCheckOptions(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                AllowCachingResponses = false,
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var response = new HealthCheckResponse()
                    {
                        Status = report.Status.ToString(),
                        Checks = report.Entries.Select(x => new HealthCheckReport
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description,
                            Exception = (x.Value.Exception != null) ? x.Value.Exception.ToString() : null,
                            Data = x.Value.Data
                        }),
                        Duration = report.TotalDuration
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response, options: new JsonSerializerOptions { IgnoreNullValues = true}));
                },
            });
            return app;
        }
    }
}

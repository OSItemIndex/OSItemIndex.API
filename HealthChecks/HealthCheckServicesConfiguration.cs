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
                    .AddCheck<OSItemIndexDbCheck>("DatabaseConnection")
                    .AddCheck<ItemsIntegrityCheck>("ItemsIntegrity");
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
                            Exception = x.Value.Exception?.ToString(),
                            Data = x.Value.Data.Count > 0 ? x.Value.Data : null
                        }),
                        Duration = report.TotalDuration
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(
                                                                               response,
                                                                               new JsonSerializerOptions
                                                                                   {IgnoreNullValues = true}));
                },
            });
            return app;
        }
    }
}

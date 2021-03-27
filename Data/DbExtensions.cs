using System;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OSItemIndex.API.Models;
using OSItemIndex.API.Repositories;

namespace OSItemIndex.API.Data
{
    public static class DbExtensions
    {
        public static IServiceCollection AddEntityFrameworkDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<DbOptions>(configuration);

            var dbOptions = configuration.Get<DbOptions>();
            services.AddDbContext<OSItemIndexDbContext>(options => options.UseNpgsql(dbOptions.DbConnectionString));

            return services;
        }

        public static IApplicationBuilder InitializeDatabases(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    using (var context = services.GetRequiredService<OSItemIndexDbContext>())
                        context.Database.EnsureCreated(); // TODO ~ Look into migrations
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                }
            }
            return app;
        }
    }
}

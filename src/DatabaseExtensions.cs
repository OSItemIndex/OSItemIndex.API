using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OSItemIndex.API.Repositories;
using Serilog;
using System;
using OSItemIndex.API.Data;
using OSItemIndex.API.Models;

namespace OSItemIndex.API
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddEntityFrameworkContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<DatabaseOptions>(configuration);

            var options = configuration.Get<DatabaseOptions>();

            services.AddDbContextPool<OSItemIndexDbContext>(builder =>
                builder.UseNpgsql(options.DbConnectionString), options.PoolSize);

            services.AddSingleton<IDbContextHelper, DbContextHelper>();
            services.AddSingleton<IEntityRepository<OSRSBoxItem>, ItemsEntityRepository>();
            services.AddSingleton<IEntityRepository<RealtimePrice>, RealtimePriceEntityRepository>();

            return services;
        }

        public static IApplicationBuilder InitializeDatabases(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    var dbContextHelper = scope.ServiceProvider.GetRequiredService<IDbContextHelper>();
                    using (var factory = dbContextHelper.GetFactory())
                    {
                        var context = factory.GetDbContext();
                        context.Database.EnsureCreated(); // TODO ~ Look into migrations
                    }
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

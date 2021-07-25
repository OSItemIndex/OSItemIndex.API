using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.ResponseCompression;
using OSItemIndex.API.Repositories;
using OSItemIndex.API.Services;
using OSItemIndex.Data;
using OSItemIndex.Data.Database;
using OSItemIndex.Data.Extensions;
using OSItemIndex.Data.Repositories;
using OSItemIndex.Data.Services;

namespace OSItemIndex.API
{
    public class Startup
    {
        private readonly string _corsOrigins = "_corsOrigins";
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            builder.Sources.Clear();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("appsettings.json", true, true);
            builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);
            builder.AddKeyPerFile("/run/secrets", true); // docker secrets - https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration-providers#key-per-file-configuration-provider
            builder.AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddEntityFrameworkContext(_configuration);
            services.AddSingleton<IDbInitializerService, DbInitializerService>();

            services.AddSingleton<IEventRepository, EventRepository>();
            services.AddSingleton<IEventService, EventService>();

            services.AddSingleton<IEntityRepository<OsrsBoxItem>, ItemsRepository>();

            services.AddSingleton<IItemsService, ItemsService>();

            services.AddSwaggerGen(c =>
            {
                var apiXml = Path.Combine(AppContext.BaseDirectory, "OSItemIndex.API.xml");
                var dataXml = Path.Combine(AppContext.BaseDirectory, "OSItemIndex.Data.xml");

                c.EnableAnnotations();
                c.IncludeXmlComments(apiXml);
                c.IncludeXmlComments(dataXml);

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OSItemIndex.API", Version = "v1" });
            }); // https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio
            // https://github.com/unchase/Unchase.Swashbuckle.AspNetCore.Extensions

            services.AddCors(options =>
            {
                options.AddPolicy(_corsOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://ositemindex.com")
                                             .WithOrigins("https://localhost")
                                             .WithOrigins("http://localhost")
                                             .AllowAnyHeader();
                                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(_corsOrigins);

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new() { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/api" } };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OSItemIndex.API v1");
            });

            app.UsePathBase("/api");
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

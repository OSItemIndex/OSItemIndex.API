using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.ResponseCompression;
using OSItemIndex.API.Repositories;
using OSItemIndex.API.Services;
using OSItemIndex.Data;
using OSItemIndex.Data.Extensions;
#pragma warning disable 1591

namespace OSItemIndex.API
{
    public class Startup
    {
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

            services.AddSingleton<IEntityRepository<OsrsBoxItem>, ItemRepository>();
            services.AddSingleton<IItemsService, ItemsService>();

            services.AddSwaggerGen(c =>
            {
                var apiXml = Path.Combine(AppContext.BaseDirectory, "OSItemIndex.API.xml");
                var dataXml = Path.Combine(AppContext.BaseDirectory, "OSItemIndex.Data.xml");

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OSItemIndex.API", Version = "v1" });
                c.IncludeXmlComments(apiXml);
                c.IncludeXmlComments(dataXml);
            }); // https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(c => { c.SerializeAsV2 = true; });
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "OSItemIndex.API v1"); });

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

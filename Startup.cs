using System;
using System.IO;
using OSItemIndex.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OSItemIndex.API.Repositories;
using OSItemIndex.API.Models;
using OSItemIndex.API.Services;

namespace OSItemIndex.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OSItemIndex.API", Version = "v1" });
                var filePath = Path.Combine(AppContext.BaseDirectory, "OSItemIndex.API.xml");
                c.IncludeXmlComments(filePath);
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddEntityFrameworkDatabases(Configuration);

            services.AddTransient<IItemsRepository, ItemsRepository>();
            services.AddTransient<IItemsService, ItemsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OSItemIndex.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.InitializeDatabases(); // use our DbExtensions to initialize the db
        }
    }
}
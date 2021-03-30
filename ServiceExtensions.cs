using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OSItemIndex.API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OSItemIndex.API
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IItemsService, ItemsService>();
            //services.AddSingleton<IPricesService, PricesService>();

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using OSItemIndex.API.Services;

namespace OSItemIndex.API
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IItemsService, ItemsService>();
            services.AddSingleton<IRealtimePricesService, RealtimePricesService>();

            return services;
        }
    }
}

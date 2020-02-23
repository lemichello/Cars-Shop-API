using CarsShop.Business.EntityServices;
using CarsShop.Business.EntityServices.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarsShop.Business.Configuration
{
    public static class BusinessServiceCollectionExtensions
    {
        public static IServiceCollection AddCarsShopBusiness(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IColorService, ColorService>();
            services.AddTransient<IEngineVolumeService, EngineVolumeService>();
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<IVendorService, VendorService>();
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IPriceHistoryService, PriceHistoryService>();

            return services;
        }
    }
}
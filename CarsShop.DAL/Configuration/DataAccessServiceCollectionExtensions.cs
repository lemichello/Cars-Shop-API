using CarsShop.DAL.Abstraction;
using CarsShop.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarsShop.DAL.Configuration
{
    public static class DataAccessServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<DbContext, EfContext>(opt => opt.UseSqlServer(configuration["ConnectionString"]));
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));

            return services;
        }
    }
}
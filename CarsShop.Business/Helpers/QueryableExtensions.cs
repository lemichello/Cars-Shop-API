using System;
using System.Linq;
using System.Linq.Expressions;
using CarsShop.Data.Entities;
using CarsShop.DTO.FiltersDto;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Business.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> cars,
            params Expression<Func<T, object>>[] includes) where T : class
        {
            return includes.Aggregate(cars, (current, include) => current.Include(include));
        }

        public static IQueryable<T> WithPagination<T>(this IQueryable<T> entities, int? index, int? size)
        {
            if (!size.HasValue || !index.HasValue)
            {
                return entities;
            }

            return entities
                .Skip(index.Value * size.Value)
                .Take(size.Value);
        }

        public static IQueryable<Car> ApplyFiltering(this IQueryable<Car> query, CarsFilter filter)
        {
            return query
                .Where(FiltersConverter.GetModelsFunc(filter))
                .Where(FiltersConverter.GetColorFunc(filter))
                .Where(FiltersConverter.GetEngineVolumeFunc(filter))
                .Where(FiltersConverter.GetPriceFunc(filter));
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CarsShop.API.Helpers
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
    }
}
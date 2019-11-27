using System;
using System.Linq;
using System.Linq.Expressions;
using CarsShop.DAL.Entities;
using CarsShop.DTO.FiltersDto;

namespace CarsShop.API.Helpers
{
    public static class FiltersConverter
    {
        public static IQueryable<Car> ApplyFiltering(this IQueryable<Car> query, CarsFilter filter)
        {
            return query
                .Where(GetModelsFunc(filter))
                .Where(GetColorFunc(filter))
                .Where(GetEngineVolumeFunc(filter))
                .Where(GetPriceFunc(filter));
        }

        private static Expression<Func<Car, bool>> GetModelsFunc(CarsFilter filter)
        {
            if (filter.ModelsId.Length != 0)
                return x => filter.ModelsId.Contains(x.ModelId);

            return x => true;
        }

        private static Expression<Func<Car, bool>> GetColorFunc(CarsFilter filter)
        {
            if (filter.ColorId != null)
                return x => x.ColorId == filter.ColorId;

            return x => true;
        }

        private static Expression<Func<Car, bool>> GetEngineVolumeFunc(CarsFilter filter)
        {
            if (filter.EngineVolumeId != null)
                return x => x.EngineVolumeId == filter.EngineVolumeId;

            return x => true;
        }

        private static Expression<Func<Car, bool>> GetPriceFunc(CarsFilter filter)
        {
            return x =>
                x.PriceHistories.First(i => filter.Price.SelectedDate > i.Date).Price >= filter.Price.FromPrice &&
                x.PriceHistories.First(i => filter.Price.SelectedDate > i.Date).Price <= filter.Price.ToPrice;
        }
    }
}
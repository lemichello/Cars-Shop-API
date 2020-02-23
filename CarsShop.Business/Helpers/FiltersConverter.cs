using System;
using System.Linq;
using System.Linq.Expressions;
using CarsShop.Data.Entities;
using CarsShop.DTO.FiltersDto;

namespace CarsShop.Business.Helpers
{
    public static class FiltersConverter
    {
        public static Expression<Func<Car, bool>> GetModelsFunc(CarsFilter filter)
        {
            if (filter.ModelsId.Length != 0)
                return x => filter.ModelsId.Contains(x.ModelId);

            return x => true;
        }

        public static Expression<Func<Car, bool>> GetColorFunc(CarsFilter filter)
        {
            if (filter.ColorId != null)
                return x => x.ColorId == filter.ColorId;

            return x => true;
        }

        public static Expression<Func<Car, bool>> GetEngineVolumeFunc(CarsFilter filter)
        {
            if (filter.EngineVolumeId != null)
                return x => x.EngineVolumeId == filter.EngineVolumeId;

            return x => true;
        }

        public static Expression<Func<Car, bool>> GetPriceFunc(CarsFilter filter)
        {
            if (filter.Price.SelectedDate == null)
            {
                return x => x.PriceHistories.OrderByDescending(i => i.Id).First().Price >= filter.Price.FromPrice &&
                            x.PriceHistories.OrderByDescending(i => i.Id).First().Price <= filter.Price.ToPrice;
            }

            return x =>
                x.PriceHistories.First(i => filter.Price.SelectedDate > i.Date).Price >=
                filter.Price.FromPrice &&
                x.PriceHistories.First(i => filter.Price.SelectedDate > i.Date).Price <=
                filter.Price.ToPrice;
        }
    }
}
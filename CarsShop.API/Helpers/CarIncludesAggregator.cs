﻿using CarsShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsShop.API.Helpers
{
    public static class CarIncludesAggregator
    {
        public static IQueryable<Car> ApplyIncludes(this IQueryable<Car> cars)
        {
            return cars.Include(i => i.Color)
                       .Include(i => i.Model)
                       .ThenInclude(i => i.Vendor)
                       .Include(i => i.EngineVolume)
                       .Include(i => i.PriceHistories);
        }
    }
}
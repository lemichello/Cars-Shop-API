using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsShop.Business.Helpers;
using CarsShop.DAL.Abstraction;
using CarsShop.Data.Entities;
using CarsShop.DTO.FiltersDto;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Business.EntityServices.Implementation
{
    public class CarService : ICarService
    {
        private readonly IRepository<Car> _carRepository;

        public CarService(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> GetCars(int? index, int? size)
        {
            return await _carRepository
                .GetAll()
                .AsNoTracking()
                .Include(x => x.Model)
                .ThenInclude(x => x.Vendor)
                .ApplyIncludes(x => x.Color, x => x.Model, x => x.EngineVolume, x => x.PriceHistories)
                .WithPagination(index, size)
                .ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetFilteredCars(CarsFilter filter, int? index, int? size)
        {
            return await _carRepository
                .GetAll()
                .AsNoTracking()
                .ApplyFiltering(filter)
                .Include(x => x.Model)
                .ThenInclude(x => x.Vendor)
                .ApplyIncludes(x => x.Color, x => x.Model, x => x.EngineVolume, x => x.PriceHistories)
                .WithPagination(index, size)
                .ToListAsync();
        }

        public async Task<Car> GetCar(int carId)
        {
            return await _carRepository
                .GetAll(i => i.Id == carId)
                .AsNoTracking()
                .Include(x => x.Model)
                .ThenInclude(x => x.Vendor)
                .ApplyIncludes(x => x.Color, x => x.Model, x => x.EngineVolume, x => x.PriceHistories)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCarsCount(CarsFilter filter)
        {
            return filter.ModelsId != null
                ? await _carRepository
                    .GetAll()
                    .ApplyFiltering(filter)
                    .CountAsync()
                : await _carRepository
                    .GetAll()
                    .CountAsync();
        }

        public async Task<float[]> GetMinAndMaxPrices()
        {
            var cars = await _carRepository
                .GetAll()
                .AsNoTracking()
                .ApplyIncludes(x => x.PriceHistories)
                .ToListAsync();

            var min = cars.Min(x => x.PriceHistories.Last().Price);
            var max = cars.Max(x => x.PriceHistories.Last().Price);

            return new[] {min, max};
        }

        public async Task AddCar(Car newCar)
        {
            await _carRepository.Add(newCar);
        }

        public async Task UpdateCar(Car car)
        {
            await _carRepository.Edit(car);
        }
    }
}
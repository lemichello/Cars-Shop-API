using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.Data.Entities;
using CarsShop.DTO.FiltersDto;

namespace CarsShop.Business.EntityServices
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetCars(int? index, int? size);
        Task<IEnumerable<Car>> GetFilteredCars(CarsFilter filter, int? index, int? size);
        Task<Car> GetCar(int carId);
        Task<int> GetCarsCount(CarsFilter filter);
        Task<float[]> GetMinAndMaxPrices();
        Task AddCar(Car newCar);
        Task UpdateCar(Car car);
    }
}
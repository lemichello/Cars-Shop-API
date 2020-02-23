using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.DAL.Abstraction;
using CarsShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Business.EntityServices.Implementation
{
    public class PriceHistoryService : IPriceHistoryService
    {
        private readonly IRepository<PriceHistory> _priceHistoryRepository;

        public PriceHistoryService(IRepository<PriceHistory> priceHistoryRepository)
        {
            _priceHistoryRepository = priceHistoryRepository;
        }

        public async Task AddPriceHistory(PriceHistory priceHistory)
        {
            await _priceHistoryRepository.Add(priceHistory);
        }

        public async Task<IEnumerable<PriceHistory>> GetByCarId(int carId)
        {
            return await _priceHistoryRepository
                .GetAll(i => i.CarId == carId)
                .ToListAsync();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.Data.Entities;

namespace CarsShop.Business.EntityServices
{
    public interface IPriceHistoryService
    {
        Task AddPriceHistory(PriceHistory priceHistory);
        Task<IEnumerable<PriceHistory>> GetByCarId(int carId);
    }
}
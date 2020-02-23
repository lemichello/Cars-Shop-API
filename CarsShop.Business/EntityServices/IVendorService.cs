using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.Data.Entities;

namespace CarsShop.Business.EntityServices
{
    public interface IVendorService
    {
        Task<IEnumerable<Vendor>> GetVendors(int? index, int? size);
        Task<Vendor> GetVendorById(int vendorId);
        Task<int> GetVendorsCount();
        Task AddVendor(Vendor newVendor);
    }
}
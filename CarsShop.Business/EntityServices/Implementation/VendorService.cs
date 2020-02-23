using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.Business.Helpers;
using CarsShop.DAL.Abstraction;
using CarsShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Business.EntityServices.Implementation
{
    public class VendorService : IVendorService
    {
        private readonly IRepository<Vendor> _vendorRepository;

        public VendorService(IRepository<Vendor> vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task<IEnumerable<Vendor>> GetVendors(int? index, int? size)
        {
            return await _vendorRepository
                .GetAll()
                .AsNoTracking()
                .WithPagination(index, size)
                .ApplyIncludes(x => x.Models)
                .ToListAsync();
        }

        public async Task<Vendor> GetVendorById(int vendorId)
        {
            return await _vendorRepository
                .GetAll(x => x.Id == vendorId)
                .AsNoTracking()
                .ApplyIncludes(x => x.Models)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetVendorsCount()
        {
            return await _vendorRepository.GetAll().CountAsync();
        }

        public async Task AddVendor(Vendor newVendor)
        {
            await _vendorRepository.Add(newVendor);
        }
    }
}
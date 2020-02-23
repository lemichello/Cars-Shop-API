using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.Business.Helpers;
using CarsShop.DAL.Abstraction;
using CarsShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Business.EntityServices.Implementation
{
    public class ModelService : IModelService
    {
        private readonly IRepository<Model> _modelRepository;

        public ModelService(IRepository<Model> modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<Model>> GetModels(int vendorId, int? index, int? size)
        {
            return await _modelRepository
                .GetAll(i => i.VendorId == vendorId)
                .AsNoTracking()
                .WithPagination(index, size)
                .ToListAsync();
        }

        public async Task<int> GetModelsCount(int vendorId)
        {
            return await _modelRepository
                .GetAll(x => x.VendorId == vendorId)
                .CountAsync();
        }

        public async Task AddModel(Model newModel)
        {
            await _modelRepository.Add(newModel);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.Data.Entities;

namespace CarsShop.Business.EntityServices
{
    public interface IModelService
    {
        Task<IEnumerable<Model>> GetModels(int vendorId, int? index, int? size);
        Task<int> GetModelsCount(int vendorId);
        Task AddModel(Model newModel);
    }
}
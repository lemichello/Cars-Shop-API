using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.Data.Entities;

namespace CarsShop.Business.EntityServices
{
    public interface IEngineVolumeService
    {
        Task<IEnumerable<EngineVolume>> GetEngineVolumes();
        Task AddEngineVolume(EngineVolume newEngineVolume);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.DAL.Abstraction;
using CarsShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Business.EntityServices.Implementation
{
    public class EngineVolumeService : IEngineVolumeService
    {
        private readonly IRepository<EngineVolume> _engineVolumeRepository;

        public EngineVolumeService(IRepository<EngineVolume> engineVolumeRepository)
        {
            _engineVolumeRepository = engineVolumeRepository;
        }

        public async Task<IEnumerable<EngineVolume>> GetEngineVolumes()
        {
            return await _engineVolumeRepository
                .GetAll()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddEngineVolume(EngineVolume newEngineVolume)
        {
            await _engineVolumeRepository.Add(newEngineVolume);
        }
    }
}
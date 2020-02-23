using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.DAL.Abstraction;
using CarsShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.Business.EntityServices.Implementation
{
    public class ColorService : IColorService
    {
        private readonly IRepository<Color> _colorsRepository;

        public ColorService(IRepository<Color> colorsRepository)
        {
            _colorsRepository = colorsRepository;
        }

        public async Task<IEnumerable<Color>> GetColors()
        {
            return await _colorsRepository
                .GetAll()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddColor(Color newColor)
        {
            await _colorsRepository.Add(newColor);
        }
    }
}
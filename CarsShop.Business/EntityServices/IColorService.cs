using System.Collections.Generic;
using System.Threading.Tasks;
using CarsShop.Data.Entities;

namespace CarsShop.Business.EntityServices
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetColors();
        Task AddColor(Color newColor);
    }
}
using System.Threading.Tasks;
using AutoMapper;
using CarsShop.DAL.Entities;
using CarsShop.DAL.Repositories.Abstraction;
using CarsShop.DTO.ColorsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IRepository<Color> _colorsRepository;
        private readonly IMapper _mapper;

        public ColorsController(IRepository<Color> colorsRepository, IMapper mapper)
        {
            _colorsRepository = colorsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var colors = await _colorsRepository
                .GetAll()
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<ColorDto[]>(colors));
        }

        [HttpPost]
        public async Task<IActionResult> AddColor([FromBody] ColorDto color)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newColor = _mapper.Map<Color>(color);

            await _colorsRepository.Add(newColor);

            return Ok(_mapper.Map<ColorDto>(newColor));
        }
    }
}

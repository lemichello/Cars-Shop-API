using System.Linq;
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
        public IActionResult GetColors()
        {
            var colors = _colorsRepository
                .GetAll()
                .AsNoTracking()
                .Select(i => _mapper.Map<ColorDto>(i))
                .ToList();

            return Ok(colors);
        }

        [HttpPost]
        public IActionResult AddColor([FromBody] ColorDto color)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newColor = _mapper.Map<Color>(color);

            _colorsRepository.Add(newColor);

            return Ok(_mapper.Map<ColorDto>(newColor));
        }
    }
}

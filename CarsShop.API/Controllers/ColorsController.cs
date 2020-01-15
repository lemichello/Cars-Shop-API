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
        public ColorsController(IRepository<Color> colorsRepository, Profile profile)
        {
            _colorsRepository = colorsRepository;
            _dtoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(profile)));
        }

        [HttpGet]
        public IActionResult GetColors()
        {
            var colors = _colorsRepository
                .GetAll()
                .AsNoTracking()
                .Select(i => _dtoMapper.Map<ColorDto>(i))
                .ToList();

            return Ok(colors);
        }

        [HttpPost]
        public IActionResult AddColor([FromBody] ColorDto color)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newColor = _dtoMapper.Map<Color>(color);

            _colorsRepository.Add(newColor);

            return Ok(_dtoMapper.Map<ColorDto>(newColor));
        }

        private readonly IRepository<Color> _colorsRepository;
        private readonly Mapper _dtoMapper;
    }
}

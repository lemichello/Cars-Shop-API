using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarsShop.DAL.Entities;
using CarsShop.DAL.Repositories.Abstraction;
using CarsShop.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        public ColorsController(IRepository<Color> colorsRepository, Profile profile)
        {
            _colorsRepository = colorsRepository;
            _dtoMapper        = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(profile)));
        }

        [HttpGet]
        public IActionResult GetColors()
        {
            var colors = _colorsRepository
                .GetAll()
                .Select(i => _dtoMapper.Map<ColorDto>(i))
                .ToList();

            return Ok(colors);
        }

        [HttpPost]
        public IActionResult AddColor([FromBody] ColorDto color)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _colorsRepository.Add(_dtoMapper.Map<Color>(color));

            return Ok();
        }

        private readonly IRepository<Color> _colorsRepository;
        private readonly Mapper             _dtoMapper;
    }
}
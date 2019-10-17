using System.Linq;
using AutoMapper;
using CarsShop.DAL.Entities;
using CarsShop.DAL.Repositories.Abstraction;
using CarsShop.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        public CarsController(IRepository<Car> repository, Profile profile)
        {
            _carsRepository = repository;
            _dtoMapper      = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(profile)));
        }

        [HttpGet]
        public IActionResult GetCars()
        {
            var cars = _carsRepository.GetAll().AsNoTracking()
                .Include(i => i.Color)
                .Include(i => i.Model)
                .ThenInclude(i => i.Vendor)
                .Include(i => i.EngineVolume)
                .Include(i => i.PriceHistories)
                .Select(i => _dtoMapper.Map<PresentationCarDto>(i));

            return Ok(cars.ToList());
        }

        private readonly IRepository<Car> _carsRepository;
        private readonly Mapper           _dtoMapper;
    }
}
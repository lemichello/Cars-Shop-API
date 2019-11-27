using System.Linq;
using AutoMapper;
using CarsShop.DAL.Entities;
using CarsShop.DAL.Repositories.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsShop.API.Helpers;
using CarsShop.DTO.CarsDto;
using CarsShop.DTO.FiltersDto;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        public CarsController(IRepository<Car> carsRepository, IRepository<PriceHistory> pricesRepository,
            Profile profile)
        {
            _carsRepository   = carsRepository;
            _pricesRepository = pricesRepository;
            _dtoMapper        = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(profile)));
        }

        [HttpGet]
        public IActionResult GetCars([FromQuery] int? index, [FromQuery] int? size)
        {
            var cars = _carsRepository
                .GetAll()
                .AsNoTracking()
                .Include(x => x.Model)
                .ThenInclude(x => x.Vendor)
                .ApplyIncludes(x => x.Color, x => x.Model, x => x.EngineVolume, x => x.PriceHistories)
                .WithPagination(index, size)
                .Select(i => _dtoMapper.Map<PresentationCarDto>(i));

            return Ok(cars.ToList());
        }

        [HttpGet]
        [Route("{carId}")]
        public IActionResult GetCar(int carId)
        {
            var car = _carsRepository
                .GetAll(i => i.Id == carId)
                .AsNoTracking()
                .Include(x => x.Model)
                .ThenInclude(x => x.Vendor)
                .ApplyIncludes(x => x.Color, x => x.Model, x => x.EngineVolume, x => x.PriceHistories)
                .FirstOrDefault();

            if (car == null)
                return NotFound();

            return Ok(_dtoMapper.Map<DetailedCarDto>(car));
        }

        [HttpGet]
        [Route("simplified/{carId}")]
        public IActionResult GetSimplifiedCar(int carId)
        {
            var car = _carsRepository.GetAll(i => i.Id == carId)
                .AsNoTracking()
                .ApplyIncludes(x => x.PriceHistories, x => x.Model)
                .FirstOrDefault();

            if (car == null)
                return NotFound();

            return Ok(_dtoMapper.Map<EditCarDto>(car));
        }

        [HttpPost]
        public IActionResult AddCar([FromBody] CarDto car)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newCar = _dtoMapper.Map<Car>(car);

            _carsRepository.Add(newCar);

            car.Id = newCar.Id;
            _pricesRepository.Add(_dtoMapper.Map<PriceHistory>(car));

            return Ok();
        }

        [HttpPut]
        [Route("{carId}")]
        public IActionResult UpdateCar(int carId, [FromBody] CarDto car)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (carId != car.Id)
            {
                return BadRequest();
            }

            var lastCarPrice = _pricesRepository.GetAll(i => i.CarId == carId).ToList().Last().Price;

            if (car.Price != lastCarPrice)
                _pricesRepository.Add(_dtoMapper.Map<PriceHistory>(car));

            _carsRepository.Edit(_dtoMapper.Map<Car>(car));

            return Ok();
        }

        [HttpDelete]
        [Route("{carId}")]
        public IActionResult DeleteCar(int carId)
        {
            var deletingCar = _carsRepository.GetAll(i => i.Id == carId).FirstOrDefault();

            if (deletingCar == null)
                return NotFound();

            _carsRepository.Remove(deletingCar);

            return Ok();
        }

        [HttpGet("count")]
        public IActionResult GetCarsCount()
        {
            return Ok(_carsRepository.GetAll().Count());
        }

        [HttpGet("min-max-prices")]
        public IActionResult GetMinAndMaxPrices()
        {
            var cars = _carsRepository
                .GetAll()
                .AsNoTracking()
                .ApplyIncludes(x => x.PriceHistories).ToList();

            var min = cars.Min(x => x.PriceHistories.Last().Price);
            var max = cars.Max(x => x.PriceHistories.Last().Price);

            return Ok(new[] {min, max});
        }

        [HttpPost("filtered")]
        public IActionResult GetFilteredCars([FromBody] CarsFilter filter, [FromQuery] int? index,
            [FromQuery] int? size)
        {
            var cars = _carsRepository
                .GetAll()
                .AsNoTracking()
                .ApplyFiltering(filter)
                .Include(x => x.Model)
                .ThenInclude(x => x.Vendor)
                .ApplyIncludes(x => x.Color, x => x.Model, x => x.EngineVolume, x => x.PriceHistories)
                .WithPagination(index, size)
                .Select(x => _dtoMapper.Map<PresentationCarDto>(x));

            return Ok(cars);
        }

        private readonly IRepository<Car>          _carsRepository;
        private readonly IRepository<PriceHistory> _pricesRepository;
        private readonly Mapper                    _dtoMapper;
    }
}
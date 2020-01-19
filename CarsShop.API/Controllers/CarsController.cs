using System.Linq;
using AutoMapper;
using CarsShop.API.Helpers;
using CarsShop.DAL.Entities;
using CarsShop.DAL.Repositories.Abstraction;
using CarsShop.DTO.CarsDto;
using CarsShop.DTO.FiltersDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IRepository<Car> _carsRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<PriceHistory> _pricesRepository;

        public CarsController(IRepository<Car> carsRepository, IRepository<PriceHistory> pricesRepository,
            IMapper mapper)
        {
            _carsRepository = carsRepository;
            _pricesRepository = pricesRepository;
            _mapper = mapper;
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
                .Select(i => _mapper.Map<CarDto>(i));

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

            return Ok(_mapper.Map<CarDto>(car));
        }

        [HttpPost]
        public IActionResult AddCar([FromBody] EditCarDto editCar)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newCar = _mapper.Map<Car>(editCar);

            _carsRepository.Add(newCar);

            editCar.Id = newCar.Id;

            _pricesRepository.Add(_mapper.Map<PriceHistory>(editCar));

            return GetCar(newCar.Id);
        }

        [HttpPut]
        [Route("{carId}")]
        public IActionResult UpdateCar(int carId, [FromBody] EditCarDto editCar)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (carId != editCar.Id)
            {
                return BadRequest();
            }

            var lastCarPrice = _pricesRepository.GetAll(i => i.CarId == carId).ToList().Last().Price;

            if (editCar.Price != lastCarPrice)
                _pricesRepository.Add(_mapper.Map<PriceHistory>(editCar));

            var updatedCar = _mapper.Map<Car>(editCar);

            _carsRepository.Edit(_mapper.Map<Car>(updatedCar));

            return GetCar(carId);
        }

        [HttpDelete]
        [Route("{carId}")]
        public IActionResult DeleteCar(int carId)
        {
            var deletingCar = _carsRepository.GetAll(i => i.Id == carId).FirstOrDefault();

            if (deletingCar == null)
                return NotFound();

            _carsRepository.Remove(deletingCar);

            return Ok(deletingCar);
        }

        [HttpPost("count")]
        public IActionResult GetCarsCount([FromBody] CarsFilter filter)
        {
            return filter.ModelsId != null
                ? Ok(_carsRepository
                    .GetAll()
                    .ApplyFiltering(filter)
                    .Count())
                : Ok(_carsRepository
                    .GetAll()
                    .Count());
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
                .Select(x => _mapper.Map<CarDto>(x));

            return Ok(cars);
        }
    }
}

using System.Linq;
using AutoMapper;
using CarsShop.DAL.Entities;
using CarsShop.DAL.Repositories.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsShop.API.Helpers;
using CarsShop.DTO.CarsDto;

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
        public IActionResult GetCars()
        {
            var cars = _carsRepository.GetAll()
                .AsNoTracking()
                .ApplyIncludes()
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
                .ApplyIncludes()
                .FirstOrDefault();

            if (car == null)
                return NotFound();

            return Ok(_dtoMapper.Map<DetailedCarDto>(car));
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

        private readonly IRepository<Car>          _carsRepository;
        private readonly IRepository<PriceHistory> _pricesRepository;
        private readonly Mapper                    _dtoMapper;
    }
}
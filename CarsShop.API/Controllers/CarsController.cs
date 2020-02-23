using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarsShop.Business.EntityServices;
using CarsShop.Data.Entities;
using CarsShop.DTO.CarsDto;
using CarsShop.DTO.FiltersDto;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        private readonly IPriceHistoryService _priceHistoryService;

        public CarsController(IMapper mapper, ICarService carService, IPriceHistoryService priceHistoryService)
        {
            _mapper = mapper;
            _carService = carService;
            _priceHistoryService = priceHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars([FromQuery] int? index, [FromQuery] int? size)
        {
            var cars = await _carService.GetCars(index, size);

            return Ok(_mapper.Map<CarDto[]>(cars));
        }

        [HttpGet]
        [Route("{carId}")]
        public async Task<IActionResult> GetCar(int carId)
        {
            var car = await _carService.GetCar(carId);

            if (car == null)
                return NotFound();

            return Ok(_mapper.Map<CarDto>(car));
        }

        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] EditCarDto editCar)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newCar = _mapper.Map<Car>(editCar);

            await _carService.AddCar(newCar);

            editCar.Id = newCar.Id;

            await _priceHistoryService.AddPriceHistory(_mapper.Map<PriceHistory>(editCar));

            return await GetCar(newCar.Id);
        }

        [HttpPut]
        [Route("{carId}")]
        public async Task<IActionResult> UpdateCar(int carId, [FromBody] EditCarDto editCar)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (carId != editCar.Id)
            {
                return BadRequest();
            }

            var prices = await _priceHistoryService.GetByCarId(carId);

            if (editCar.Price != prices.Last().Price)
                await _priceHistoryService.AddPriceHistory(_mapper.Map<PriceHistory>(editCar));

            var updatedCar = _mapper.Map<Car>(editCar);

            await _carService.UpdateCar(_mapper.Map<Car>(updatedCar));

            return await GetCar(carId);
        }

        [HttpPost("count")]
        public async Task<IActionResult> GetCarsCount([FromBody] CarsFilter filter)
        {
            return Ok(await _carService.GetCarsCount(filter));
        }

        [HttpGet("min-max-prices")]
        public async Task<IActionResult> GetMinAndMaxPrices()
        {
            return Ok(await _carService.GetMinAndMaxPrices());
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetFilteredCars([FromBody] CarsFilter filter, [FromQuery] int? index,
            [FromQuery] int? size)
        {
            var cars = await _carService.GetFilteredCars(filter, index, size);

            return Ok(_mapper.Map<CarDto[]>(cars));
        }
    }
}
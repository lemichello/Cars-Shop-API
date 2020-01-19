using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetCars([FromQuery] int? index, [FromQuery] int? size)
        {
            var cars = await _carsRepository
                .GetAll()
                .AsNoTracking()
                .Include(x => x.Model)
                .ThenInclude(x => x.Vendor)
                .ApplyIncludes(x => x.Color, x => x.Model, x => x.EngineVolume, x => x.PriceHistories)
                .WithPagination(index, size)
                .ToListAsync();

            return Ok(_mapper.Map<CarDto[]>(cars));
        }

        [HttpGet]
        [Route("{carId}")]
        public async Task<IActionResult> GetCar(int carId)
        {
            var car = await _carsRepository
                .GetAll(i => i.Id == carId)
                .AsNoTracking()
                .Include(x => x.Model)
                .ThenInclude(x => x.Vendor)
                .ApplyIncludes(x => x.Color, x => x.Model, x => x.EngineVolume, x => x.PriceHistories)
                .FirstOrDefaultAsync();

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

            await _carsRepository.Add(newCar);

            editCar.Id = newCar.Id;

            await _pricesRepository.Add(_mapper.Map<PriceHistory>(editCar));

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

            var prices = await _pricesRepository
                .GetAll(i => i.CarId == carId)
                .ToListAsync();

            if (editCar.Price != prices.Last().Price)
                await _pricesRepository.Add(_mapper.Map<PriceHistory>(editCar));

            var updatedCar = _mapper.Map<Car>(editCar);

            await _carsRepository.Edit(_mapper.Map<Car>(updatedCar));

            return await GetCar(carId);
        }

        [HttpDelete]
        [Route("{carId}")]
        public async Task<IActionResult> DeleteCar(int carId)
        {
            var deletingCar = await _carsRepository.GetAll(i => i.Id == carId).FirstOrDefaultAsync();

            if (deletingCar == null)
                return NotFound();

            await _carsRepository.Remove(deletingCar);

            return Ok(deletingCar);
        }

        [HttpPost("count")]
        public async Task<IActionResult> GetCarsCount([FromBody] CarsFilter filter)
        {
            return filter.ModelsId != null
                ? Ok(await _carsRepository
                    .GetAll()
                    .ApplyFiltering(filter)
                    .CountAsync())
                : Ok(await _carsRepository
                    .GetAll()
                    .CountAsync());
        }

        [HttpGet("min-max-prices")]
        public async Task<IActionResult> GetMinAndMaxPrices()
        {
            var cars = await _carsRepository
                .GetAll()
                .AsNoTracking()
                .ApplyIncludes(x => x.PriceHistories)
                .ToListAsync();

            var min = cars.Min(x => x.PriceHistories.Last().Price);
            var max = cars.Max(x => x.PriceHistories.Last().Price);

            return Ok(new[] {min, max});
        }

        [HttpPost("filtered")]
        public async Task<IActionResult> GetFilteredCars([FromBody] CarsFilter filter, [FromQuery] int? index,
            [FromQuery] int? size)
        {
            var cars = await _carsRepository
                .GetAll()
                .AsNoTracking()
                .ApplyFiltering(filter)
                .Include(x => x.Model)
                .ThenInclude(x => x.Vendor)
                .ApplyIncludes(x => x.Color, x => x.Model, x => x.EngineVolume, x => x.PriceHistories)
                .WithPagination(index, size)
                .ToListAsync();

            return Ok(_mapper.Map<CarDto[]>(cars));
        }
    }
}

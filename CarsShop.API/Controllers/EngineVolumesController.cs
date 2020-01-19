using System.Threading.Tasks;
using AutoMapper;
using CarsShop.DAL.Entities;
using CarsShop.DAL.Repositories.Abstraction;
using CarsShop.DTO.EngineVolumesDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineVolumesController : ControllerBase
    {
        private readonly IRepository<EngineVolume> _engineVolumesRepository;
        private readonly IMapper _mapper;

        public EngineVolumesController(IRepository<EngineVolume> engineVolumesRepository, IMapper mapper)
        {
            _engineVolumesRepository = engineVolumesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEngineVolumes()
        {
            var engineVolumes = await _engineVolumesRepository
                .GetAll()
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<EngineVolumeDto[]>(engineVolumes));
        }

        [HttpPost]
        public async Task<IActionResult> AddEngineVolume([FromBody] EngineVolumeDto engineVolume)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newEngineVolume = _mapper.Map<EngineVolume>(engineVolume);

            await _engineVolumesRepository.Add(newEngineVolume);

            return Ok(_mapper.Map<EngineVolumeDto>(newEngineVolume));
        }
    }
}

using System.Linq;
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
        public IActionResult GetEngineVolumes()
        {
            var engineVolumes = _engineVolumesRepository
                .GetAll()
                .AsNoTracking()
                .Select(i => _mapper.Map<EngineVolumeDto>(i))
                .ToList();

            return Ok(engineVolumes);
        }

        [HttpPost]
        public IActionResult AddEngineVolume([FromBody] EngineVolumeDto engineVolume)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newEngineVolume = _mapper.Map<EngineVolume>(engineVolume);

            _engineVolumesRepository.Add(newEngineVolume);

            return Ok(_mapper.Map<EngineVolumeDto>(newEngineVolume));
        }
    }
}

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
        public EngineVolumesController(IRepository<EngineVolume> engineVolumesRepository, Profile profile)
        {
            _engineVolumesRepository = engineVolumesRepository;
            _dtoMapper               = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(profile)));
        }

        [HttpGet]
        public IActionResult GetEngineVolumes()
        {
            var engineVolumes = _engineVolumesRepository
                .GetAll()
                .AsNoTracking()
                .Select(i => _dtoMapper.Map<EngineVolumeDto>(i))
                .ToList();

            return Ok(engineVolumes);
        }

        [HttpPost]
        public IActionResult AddEngineVolume([FromBody] EngineVolumeDto engineVolume)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _engineVolumesRepository.Add(_dtoMapper.Map<EngineVolume>(engineVolume));

            return Ok();
        }

        private readonly IRepository<EngineVolume> _engineVolumesRepository;
        private readonly Mapper                    _dtoMapper;
    }
}
using System.Threading.Tasks;
using AutoMapper;
using CarsShop.Business.EntityServices;
using CarsShop.Data.Entities;
using CarsShop.DTO.EngineVolumesDto;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineVolumesController : ControllerBase
    {
        private readonly IEngineVolumeService _engineVolumeService;
        private readonly IMapper _mapper;

        public EngineVolumesController(IMapper mapper, IEngineVolumeService engineVolumeService)
        {
            _mapper = mapper;
            _engineVolumeService = engineVolumeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEngineVolumes()
        {
            var engineVolumes = await _engineVolumeService.GetEngineVolumes();

            return Ok(_mapper.Map<EngineVolumeDto[]>(engineVolumes));
        }

        [HttpPost]
        public async Task<IActionResult> AddEngineVolume([FromBody] EngineVolumeDto engineVolume)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newEngineVolume = _mapper.Map<EngineVolume>(engineVolume);

            await _engineVolumeService.AddEngineVolume(newEngineVolume);

            return Ok(_mapper.Map<EngineVolumeDto>(newEngineVolume));
        }
    }
}
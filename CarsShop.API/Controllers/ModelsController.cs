using System.Threading.Tasks;
using AutoMapper;
using CarsShop.API.Helpers;
using CarsShop.DAL.Entities;
using CarsShop.DAL.Repositories.Abstraction;
using CarsShop.DTO.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Model> _modelsRepository;

        public ModelsController(IRepository<Model> modelsRepository, IMapper mapper)
        {
            _modelsRepository = modelsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{vendorId}")]
        public async Task<IActionResult> GetModels(int vendorId, [FromQuery] int? index, [FromQuery] int? size)
        {
            var models = await _modelsRepository
                .GetAll(i => i.VendorId == vendorId)
                .AsNoTracking()
                .WithPagination(index, size)
                .ToListAsync();

            return Ok(_mapper.Map<ModelDto[]>(models));
        }

        [HttpPost]
        public async Task<IActionResult> AddModel([FromBody] ModelDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newModel = _mapper.Map<Model>(model);

            await _modelsRepository.Add(newModel);

            return Ok(_mapper.Map<ModelDto>(newModel));
        }

        [HttpGet("count/{vendorId}")]
        public async Task<IActionResult> GetModelsCount(int vendorId)
        {
            return Ok(await _modelsRepository
                .GetAll(x => x.VendorId == vendorId)
                .CountAsync());
        }
    }
}

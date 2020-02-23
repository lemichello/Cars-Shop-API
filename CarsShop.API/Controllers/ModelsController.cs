using System.Threading.Tasks;
using AutoMapper;
using CarsShop.Business.EntityServices;
using CarsShop.Data.Entities;
using CarsShop.DTO.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IModelService _modelService;

        public ModelsController(IMapper mapper, IModelService modelService)
        {
            _mapper = mapper;
            _modelService = modelService;
        }

        [HttpGet]
        [Route("{vendorId}")]
        public async Task<IActionResult> GetModels(int vendorId, [FromQuery] int? index, [FromQuery] int? size)
        {
            var models = await _modelService.GetModels(vendorId, index, size);

            return Ok(_mapper.Map<ModelDto[]>(models));
        }

        [HttpPost]
        public async Task<IActionResult> AddModel([FromBody] ModelDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newModel = _mapper.Map<Model>(model);

            await _modelService.AddModel(newModel);

            return Ok(_mapper.Map<ModelDto>(newModel));
        }

        [HttpGet("count/{vendorId}")]
        public async Task<IActionResult> GetModelsCount(int vendorId)
        {
            return Ok(await _modelService.GetModelsCount(vendorId));
        }
    }
}
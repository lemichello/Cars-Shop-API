using System.Linq;
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
        public IActionResult GetModels(int vendorId, [FromQuery] int? index, [FromQuery] int? size)
        {
            var models = _modelsRepository
                .GetAll(i => i.VendorId == vendorId)
                .AsNoTracking()
                .WithPagination(index, size)
                .Select(i => _mapper.Map<ModelDto>(i))
                .ToList();

            return Ok(models);
        }

        [HttpPost]
        public IActionResult AddModel([FromBody] ModelDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newModel = _mapper.Map<Model>(model);

            _modelsRepository.Add(newModel);

            return Ok(_mapper.Map<ModelDto>(newModel));
        }

        [HttpGet("count/{vendorId}")]
        public IActionResult GetModelsCount(int vendorId)
        {
            return Ok(_modelsRepository
                .GetAll(x => x.VendorId == vendorId)
                .Count());
        }
    }
}

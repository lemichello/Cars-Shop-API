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
        public ModelsController(IRepository<Model> modelsRepository, Profile profile)
        {
            _modelsRepository = modelsRepository;
            _dtoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(profile)));
        }

        [HttpGet]
        [Route("{vendorId}")]
        public IActionResult GetModels(int vendorId, [FromQuery] int? index, [FromQuery] int? size)
        {
            var models = _modelsRepository
                .GetAll(i => i.VendorId == vendorId)
                .AsNoTracking()
                .WithPagination(index, size)
                .Select(i => _dtoMapper.Map<ModelDto>(i))
                .ToList();

            return Ok(models);
        }

        [HttpPost]
        public IActionResult AddModel([FromBody] ModelDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newModel = _dtoMapper.Map<Model>(model);

            _modelsRepository.Add(newModel);

            return Ok(_dtoMapper.Map<ModelDto>(newModel));
        }

        [HttpGet("count")]
        public IActionResult GetModelsCount()
        {
            return Ok(_modelsRepository.GetAll().Count());
        }

        private readonly IRepository<Model> _modelsRepository;
        private readonly Mapper _dtoMapper;
    }
}

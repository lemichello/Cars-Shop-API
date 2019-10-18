using System.Linq;
using AutoMapper;
using CarsShop.DAL.Entities;
using CarsShop.DAL.Repositories.Abstraction;
using CarsShop.DTO;
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
            _dtoMapper        = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(profile)));
        }

        [HttpGet]
        [Route("{vendorId}")]
        public IActionResult GetModels(int vendorId)
        {
            var models = _modelsRepository
                .GetAll(i => i.VendorId == vendorId)
                .AsNoTracking()
                .Select(i => _dtoMapper.Map<ModelDto>(i))
                .ToList();

            return Ok(models);
        }

        [HttpPost]
        public IActionResult AddModel([FromBody] ModelDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _modelsRepository.Add(_dtoMapper.Map<Model>(model));

            return Ok();
        }

        private readonly IRepository<Model> _modelsRepository;
        private readonly Mapper             _dtoMapper;
    }
}
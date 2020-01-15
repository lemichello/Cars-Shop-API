using System.Linq;
using AutoMapper;
using CarsShop.API.Helpers;
using CarsShop.DAL.Entities;
using CarsShop.DAL.Repositories.Abstraction;
using CarsShop.DTO.VendorsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        public VendorsController(IRepository<Vendor> vendorsRepository, Profile profile)
        {
            _vendorsRepository = vendorsRepository;
            _dtoMapper         = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(profile)));
        }

        [HttpGet]
        public IActionResult GetVendors([FromQuery] int? index, [FromQuery] int? size)
        {
            var vendors = _vendorsRepository
                .GetAll()
                .AsNoTracking()
                .WithPagination(index, size)
                .ApplyIncludes(x => x.Models);

            return Ok(_dtoMapper.Map<VendorDto[]>(vendors));
        }

        [HttpPost]
        public ActionResult AddVendor([FromBody] VendorDto vendor)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _vendorsRepository.Add(_dtoMapper.Map<Vendor>(vendor));

            return Ok();
        }

        [HttpGet("count")]
        public IActionResult GetVendorsCount()
        {
            return Ok(_vendorsRepository.GetAll().Count());
        }

        private readonly IRepository<Vendor> _vendorsRepository;
        private readonly Mapper              _dtoMapper;
    }
}
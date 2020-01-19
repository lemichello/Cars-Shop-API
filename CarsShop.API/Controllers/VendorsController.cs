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
        private readonly IMapper _mapper;
        private readonly IRepository<Vendor> _vendorsRepository;

        public VendorsController(IRepository<Vendor> vendorsRepository, IMapper mapper)
        {
            _vendorsRepository = vendorsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetVendors([FromQuery] int? index, [FromQuery] int? size)
        {
            var vendors = _vendorsRepository
                .GetAll()
                .AsNoTracking()
                .WithPagination(index, size)
                .ApplyIncludes(x => x.Models);

            return Ok(_mapper.Map<VendorDto[]>(vendors));
        }

        [HttpPost]
        public ActionResult AddVendor([FromBody] VendorDto vendor)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newVendor = _mapper.Map<Vendor>(vendor);

            _vendorsRepository.Add(newVendor);

            return Ok(_mapper.Map<VendorDto>(newVendor));
        }

        [HttpGet("count")]
        public IActionResult GetVendorsCount()
        {
            return Ok(_vendorsRepository.GetAll().Count());
        }
    }
}

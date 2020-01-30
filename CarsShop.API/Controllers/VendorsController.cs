using System.Threading.Tasks;
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
        public async Task<IActionResult> GetVendors([FromQuery] int? index, [FromQuery] int? size)
        {
            var vendors = await _vendorsRepository
                .GetAll()
                .AsNoTracking()
                .WithPagination(index, size)
                .ApplyIncludes(x => x.Models)
                .ToListAsync();

            return Ok(_mapper.Map<VendorDto[]>(vendors));
        }

        [HttpGet]
        [Route("{vendorId}")]
        public async Task<IActionResult> GetVendorById(int vendorId)
        {
            var vendor = await _vendorsRepository
                .GetAll(x => x.Id == vendorId)
                .AsNoTracking()
                .ApplyIncludes(x => x.Models)
                .FirstOrDefaultAsync();

            return Ok(_mapper.Map<VendorDto>(vendor));
        }

        [HttpPost]
        public async Task<ActionResult> AddVendor([FromBody] VendorDto vendor)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newVendor = _mapper.Map<Vendor>(vendor);

            await _vendorsRepository.Add(newVendor);

            return Ok(_mapper.Map<VendorDto>(newVendor));
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetVendorsCount()
        {
            return Ok(await _vendorsRepository.GetAll().CountAsync());
        }
    }
}

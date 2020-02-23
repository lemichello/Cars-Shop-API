using System.Threading.Tasks;
using AutoMapper;
using CarsShop.Business.EntityServices;
using CarsShop.Data.Entities;
using CarsShop.DTO.VendorsDto;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVendorService _vendorService;

        public VendorsController(IMapper mapper, IVendorService vendorService)
        {
            _mapper = mapper;
            _vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVendors([FromQuery] int? index, [FromQuery] int? size)
        {
            var vendors = await _vendorService.GetVendors(index, size);

            return Ok(_mapper.Map<VendorDto[]>(vendors));
        }

        [HttpGet]
        [Route("{vendorId}")]
        public async Task<IActionResult> GetVendorById(int vendorId)
        {
            var vendor = await _vendorService.GetVendorById(vendorId);

            return Ok(_mapper.Map<VendorDto>(vendor));
        }

        [HttpPost]
        public async Task<ActionResult> AddVendor([FromBody] VendorDto vendor)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newVendor = _mapper.Map<Vendor>(vendor);

            await _vendorService.AddVendor(newVendor);

            return Ok(_mapper.Map<VendorDto>(newVendor));
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetVendorsCount()
        {
            return Ok(await _vendorService.GetVendorsCount());
        }
    }
}
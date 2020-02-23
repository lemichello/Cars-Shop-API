using System.Threading.Tasks;
using AutoMapper;
using CarsShop.Business.EntityServices;
using CarsShop.Data.Entities;
using CarsShop.DTO.ColorsDto;
using Microsoft.AspNetCore.Mvc;

namespace CarsShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;
        private readonly IMapper _mapper;

        public ColorsController(IMapper mapper, IColorService colorService)
        {
            _mapper = mapper;
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var colors = await _colorService.GetColors();

            return Ok(_mapper.Map<ColorDto[]>(colors));
        }

        [HttpPost]
        public async Task<IActionResult> AddColor([FromBody] ColorDto color)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newColor = _mapper.Map<Color>(color);

            await _colorService.AddColor(newColor);

            return Ok(_mapper.Map<ColorDto>(newColor));
        }
    }
}
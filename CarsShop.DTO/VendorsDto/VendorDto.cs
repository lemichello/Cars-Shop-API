using System.Collections.Generic;
using CarsShop.DTO.ModelsDto;

namespace CarsShop.DTO.VendorsDto
{
    public class VendorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ModelDto> Models { get; set; }
    }
}

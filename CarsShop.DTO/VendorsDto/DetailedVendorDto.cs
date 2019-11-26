using System.Collections.Generic;
using CarsShop.DTO.ModelsDto;

namespace CarsShop.DTO.VendorsDto
{
    public class DetailedVendorDto : VendorDto
    {
        public IEnumerable<ModelDto> Models { get; set; }
    }
}
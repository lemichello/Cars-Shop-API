using System.Collections.Generic;
using CarsShop.DTO.ColorsDto;
using CarsShop.DTO.EngineVolumesDto;
using CarsShop.DTO.ModelsDto;
using CarsShop.DTO.PriceHistoriesDto;
using CarsShop.DTO.VendorsDto;

namespace CarsShop.DTO.CarsDto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ModelDto Model { get; set; }
        public VendorDto Vendor { get; set; }
        public ColorDto Color { get; set; }
        public EngineVolumeDto EngineVolume { get; set; }
        public IEnumerable<PriceHistoryDto> PricesHistory { get; set; }
    }
}
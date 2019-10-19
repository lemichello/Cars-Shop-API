using System.Collections.Generic;
using CarsShop.DTO.PriceHistoriesDto;

namespace CarsShop.DTO.CarsDto
{
    public class DetailedCarDto : BaseCarDto
    {
        public string Model { get; set; }
        public string Vendor { get; set; }
        public string Color { get; set; }
        public float EngineVolume { get; set; }
        public ICollection<PriceHistoryDto> PricesHistory { get; set; }
    }
}
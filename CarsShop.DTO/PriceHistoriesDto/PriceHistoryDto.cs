using System;

namespace CarsShop.DTO.PriceHistoriesDto
{
    public class PriceHistoryDto
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
    }
}

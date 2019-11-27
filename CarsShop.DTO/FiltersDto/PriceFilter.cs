using System;

namespace CarsShop.DTO.FiltersDto
{
    public class PriceFilter
    {
        public float    FromPrice    { get; set; }
        public float    ToPrice      { get; set; }
        public DateTime SelectedDate { get; set; }
    }
}
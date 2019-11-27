namespace CarsShop.DTO.FiltersDto
{
    public class CarsFilter
    {
        public int[]       ModelsId       { get; set; }
        public int?        ColorId        { get; set; }
        public int?        EngineVolumeId { get; set; }
        public PriceFilter Price          { get; set; }
    }
}
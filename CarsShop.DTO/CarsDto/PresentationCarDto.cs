namespace CarsShop.DTO.CarsDto
{
    public class PresentationCarDto : BaseCarDto
    {
        public string Model { get; set; }
        public string Vendor { get; set; }
        public string Color { get; set; }
        public float EngineVolume { get; set; }
        public float Price { get; set; }
    }
}
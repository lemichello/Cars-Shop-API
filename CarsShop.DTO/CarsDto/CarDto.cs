namespace CarsShop.DTO.CarsDto
{
    public class CarDto : BaseCarDto
    {
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int EngineVolumeId { get; set; }
        public float Price { get; set; }
    }
}
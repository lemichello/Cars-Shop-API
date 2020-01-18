namespace CarsShop.DTO.CarsDto
{
    public class EditCarDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int EngineVolumeId { get; set; }
        public float Price { get; set; }
    }
}
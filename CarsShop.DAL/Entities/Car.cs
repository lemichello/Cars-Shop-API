using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarsShop.DAL.Entities
{
    public class Car
    {
        public int Id { get; set; }
        [MaxLength(150)] public string Description { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int EngineVolumeId { get; set; }

        public Model Model { get; set; }
        public Color Color { get; set; }
        public EngineVolume EngineVolume { get; set; }
        public ICollection<PriceHistory> PriceHistories { get; set; }
    }
}
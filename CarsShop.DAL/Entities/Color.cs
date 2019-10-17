using System.ComponentModel.DataAnnotations;

namespace CarsShop.DAL.Entities
{
    public class Color
    {
        public int Id { get; set; }
        [MaxLength(25)] public string Name { get; set; }
    }
}
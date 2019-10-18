using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarsShop.DAL.Entities
{
    public class Vendor
    {
        public int Id { get; set; }
        [MaxLength(20)] public string Name { get; set; }

        public ICollection<Model> Models { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace CarsShop.Data.Entities
{
    public class Model
    {
        public int Id { get; set; }
        [MaxLength(30)] public string Name { get; set; }
        public int VendorId { get; set; }

        public Vendor Vendor { get; set; }
    }
}
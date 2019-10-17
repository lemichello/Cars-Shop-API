using System;

namespace CarsShop.DAL.Entities
{
    public class PriceHistory
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public int CarId { get; set; }
        
        public Car Car { get; set; }
    }
}
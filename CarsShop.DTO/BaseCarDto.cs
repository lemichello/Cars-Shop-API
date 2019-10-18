using System;
using System.Collections.Generic;
using System.Text;

namespace CarsShop.DTO
{
    public abstract class BaseCarDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}

using CarsShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsShop.DAL
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<EngineVolume> EngineVolumes { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<PriceHistory> PriceHistories { get; set; }
    }
}
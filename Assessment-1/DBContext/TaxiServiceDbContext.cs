using Assessment_1.Entity;
using Microsoft.EntityFrameworkCore;

namespace Assessment_1.DBContext
{
    public class TaxiServiceDbContext: DbContext
    {
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public TaxiServiceDbContext(DbContextOptions<TaxiServiceDbContext> options) : base(options)
        {
        }
    }
}

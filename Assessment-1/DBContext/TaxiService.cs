using Assessment_1.Entitites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Assessment_1.DBContext
{
    public class TaxiService : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<VehicleAndAvailability> VehiclesAndAvailability { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Ratings> Ratings { get; set; }

        public TaxiService(DbContextOptions<TaxiService> options) : base(options)
        {
        }
    }
}

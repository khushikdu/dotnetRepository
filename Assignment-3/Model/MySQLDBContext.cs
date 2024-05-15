using Microsoft.EntityFrameworkCore;


namespace Assignment_3.Model
{
    public class MySQLDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public MySQLDBContext(DbContextOptions<MySQLDBContext> options) : base(options) 
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Assignment_3.Model
{
    /// <summary>
    /// Represents the database context for the MySQL database.
    /// </summary>
    public class MySQLDBContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet for movies.
        /// </summary>
        public DbSet<Movie> Movies { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for customers.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for rentals.
        /// </summary>
        public DbSet<Rental> Rentals { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MySQLDBContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public MySQLDBContext(DbContextOptions<MySQLDBContext> options) : base(options)
        {
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Model
{
    /// <summary>
    /// Represents a rental entity.
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Gets or sets the ID of the rental.
        /// </summary>
        /// <value>The ID of the rental.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the movie associated with the rental.
        /// </summary>
        /// <value>The ID of the movie.</value>
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets the movie associated with the rental.
        /// </summary>
        /// <value>The movie associated with the rental.</value>
        public Movie Movie { get; set; }

        /// <summary>
        /// Gets or sets the ID of the customer associated with the rental.
        /// </summary>
        /// <value>The ID of the customer.</value>
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with the rental.
        /// </summary>
        /// <value>The customer associated with the rental.</value>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the rental date.
        /// </summary>
        /// <value>The rental date.</value>
        public DateTime RentalDate { get; set; }

        /// <summary>
        /// Gets or sets the return date.
        /// </summary>
        /// <value>The return date.</value>
        public DateTime? ReturnDate { get; set; }
    }
}

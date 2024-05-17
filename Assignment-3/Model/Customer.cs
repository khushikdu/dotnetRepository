using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Assignment_3.Model
{
    /// <summary>
    /// Represents a customer entity.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the ID of the customer.
        /// </summary>
        /// <value>The ID of the customer.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the customer.
        /// </summary>
        /// <value>The username of the customer.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        /// <value>The email address of the customer.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the rentals associated with the customer.
        /// </summary>
        /// <value>A collection of rentals associated with the customer.</value>
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}

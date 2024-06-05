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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}

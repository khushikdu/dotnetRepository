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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Assignment_3.Model
{
    /// <summary>
    /// Represents a movie entity.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Gets or sets the ID of the movie.
        /// </summary>
        /// <value>The ID of the movie.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the movie.
        /// </summary>
        /// <value>The title of the movie.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the price of the movie.
        /// </summary>
        /// <value>The price of the movie.</value>
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the rentals associated with the movie.
        /// </summary>
        /// <value>A collection of rentals associated with the movie.</value>
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}

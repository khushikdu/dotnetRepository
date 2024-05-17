using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    /// <summary>
    /// Data transfer object for renting a movie by its ID.
    /// </summary>
    public class RentMovieByID_DTO
    {
        /// <summary>
        /// Gets or sets the ID of the movie to be rented.
        /// </summary>
        /// <value>The ID of the movie.</value>
        [Required]
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the customer renting the movie.
        /// </summary>
        /// <value>The ID of the customer.</value>
        [Required]
        public int CustomerId { get; set; }
    }
}

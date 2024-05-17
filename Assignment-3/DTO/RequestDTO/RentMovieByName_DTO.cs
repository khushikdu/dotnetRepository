using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    /// <summary>
    /// Data transfer object for renting a movie by its title.
    /// </summary>
    public class RentMovieByName_DTO
    {
        /// <summary>
        /// Gets or sets the title of the movie to be rented.
        /// </summary>
        /// <value>The title of the movie.</value>
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters long.")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the username of the customer renting the movie.
        /// </summary>
        /// <value>The username of the customer.</value>
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters long.")]
        public string Username { get; set; }
    }
}

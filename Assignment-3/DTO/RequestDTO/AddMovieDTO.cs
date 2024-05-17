using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    /// <summary>
    /// Data transfer object for adding a new movie.
    /// </summary>
    public class AddMovieDTO
    {
        /// <summary>
        /// Gets or sets the title of the movie.
        /// </summary>
        /// <value>The title must be between 3 and 100 characters long.</value>
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters long.")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the price of the movie.
        /// </summary>
        /// <value>The price must be specified.</value>
        [Required]
        public int Price { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    /// <summary>
    /// Data transfer object for adding a new movie.
    /// </summary>
    public class AddMovieDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters long.")]
        public string Title { get; set; }

        [Required]
        public int Price { get; set; }
    }
}

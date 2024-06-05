using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    /// <summary>
    /// Data transfer object for renting a movie by its title.
    /// </summary>
    public class RentMovieByName_DTO
    {

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters long.")]
        public string Title { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters long.")]
        public string Username { get; set; }
    }
}

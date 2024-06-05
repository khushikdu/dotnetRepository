using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    /// <summary>
    /// Data transfer object for renting a movie by its ID.
    /// </summary>
    public class RentMovieByID_DTO
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    public class RentMovieByID_DTO
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}

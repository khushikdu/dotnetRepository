using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    public class RentMovieByName_DTO
    {
        [Required]
        [StringLength(100), MinLength(3)]
        public string Title { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        public string Username { get; set; }
    }
}

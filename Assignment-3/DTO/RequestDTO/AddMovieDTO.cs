using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    public class AddMovieDTO
    {
        [Required]
        [StringLength(100), MinLength(3)]
        public string Title { get; set; }
        [Required]
        public int Price { get; set; }
    }
}

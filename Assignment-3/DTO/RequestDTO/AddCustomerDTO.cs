using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    public class AddCustomerDTO
    {
        [Required]
        [StringLength(100),MinLength(3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email Address.")]
        public string Email { get; set; }
    }
}

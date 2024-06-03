using Assignment_2.Validations;
using System.ComponentModel.DataAnnotations;

//Didn't change the nomenclature to ViewModel as the test cases were failing due to the previous namespace
namespace Assignment_2.DTO
{
    /// <summary>
    /// Represents the data transfer object (DTO) for user registration information.
    /// </summary>
    public class UserDTO
    {
        [Required]
        [StringLength(50)]
        [UsernameValidation]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [UniqueEmail]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [PasswordValidation]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        public string? Role { get; set; }
    }
}

using Assignment_2.Validations;
using System.ComponentModel.DataAnnotations;

namespace Assignment_2.DTO
{
    /// <summary>
    /// Represents the data transfer object (DTO) for user registration information.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Gets or sets the username for registration.
        /// </summary>
        [Required]
        [StringLength(50)]
        [UsernameValidation]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address for registration.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password for registration.
        /// </summary>
        [Required]
        [MinLength(8)]
        [PasswordValidation]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the user.
        /// </summary>
        [StringLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        [Required(ErrorMessage = "Role is Required")]
        public string? Role { get; set; }
    }
}

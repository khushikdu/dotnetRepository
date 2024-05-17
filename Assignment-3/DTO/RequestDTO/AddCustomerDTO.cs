using System.ComponentModel.DataAnnotations;

namespace Assignment_3.DTO.RequestDTO
{
    /// <summary>
    /// Data transfer object for adding a new customer.
    /// </summary>
    public class AddCustomerDTO
    {
        /// <summary>
        /// Gets or sets the username of the customer.
        /// </summary>
        /// <value>The username must be between 3 and 100 characters long.</value>
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters long.")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        /// <value>The email address must be a valid email format.</value>
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
    }
}

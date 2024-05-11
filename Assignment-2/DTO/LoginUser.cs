using System.ComponentModel.DataAnnotations;

namespace Assignment_2.DTO
{
    /// <summary>
    /// Represents the data transfer object (DTO) for user login information.
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// Gets or sets the username for login.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for login.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}

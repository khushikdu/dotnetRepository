using System.ComponentModel.DataAnnotations;

namespace Assignment_2.DTO
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Assessment_1.ViewModel.RequestVM
{
    public class LoginDriverVM
    {
        [EmailAddress]
        [Required(ErrorMessage = "Username is required.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}

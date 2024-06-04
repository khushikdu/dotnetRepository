using Assignment_2.Validations;
using System.ComponentModel.DataAnnotations;

namespace Assessment_1.ViewModel.RequestVM
{
    public class AddRiderVM
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters long.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        //[UniqueEmail]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [PasswordValidation]
        public string Password { get; set; }
        [Required]  
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; }
    }
}

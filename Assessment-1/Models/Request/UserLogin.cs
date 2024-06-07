using System.ComponentModel.DataAnnotations;
using Assessment_1.Enums;
using Assessment_1.Validations;

namespace Assessment_1.Models.Request
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Email or Phone number is required")]
        public string EmailOrPhone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "User type is required")]
        [UserTypeValidation]
        public string UserType { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Assessment_1.Enums;
using Assessment_1.Validations;

namespace Assessment_1.Models.Request
{
    public class AddDriver
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
        public UserType? UserType { get; set; }

        [Required(ErrorMessage = "Vehicle type is required")]
        [VehicleTypeValidation]
        public string VehicleType { get; set; }

        [Required(ErrorMessage = "Vehicle number is required")]
        [MaxLength(20, ErrorMessage = "Vehicle number cannot exceed 20 characters")]
        public string VehicleNumber { get; set; }
    }
}

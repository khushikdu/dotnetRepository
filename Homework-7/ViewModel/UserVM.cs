using Homework_7.Validators;
using System.ComponentModel.DataAnnotations;

namespace Homework_7.ViewModel
{
    /// <summary>
    /// View Model (VM) for user registration.
    /// </summary>
    public class UserVM
    {

        [Required(ErrorMessage = "Username is a required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 to 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email Address is a required field.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is a required field.")]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Age is a required field.")]
        [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Phone number is a required field.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }


        [ValidCountry(ErrorMessage = "Invalid country. Valid countries are: USA, Canada, UK, Australia.")]
        public string? Country { get; set; }

        [ValidGender(ErrorMessage = "Invalid gender. Valid genders are: Male, Female, Other.")]
        public string? Gender { get; set; }

        [CreditCardValidator(ErrorMessage = "Invalid Credit Card Number")]
        public string? CreditCard { get; set; }

        [ExpirationDateValidator(ErrorMessage = "Invalid expiration date. Expiration date must be a future date in the format MM/YYYY.")]
        public string? ExpirationDate { get; set; }
        [CVVValidator(ErrorMessage = "Invalid CVV. CVV must be a 3 or 4-digit number.")]
        public string? CVV { get; set; }
    }
}

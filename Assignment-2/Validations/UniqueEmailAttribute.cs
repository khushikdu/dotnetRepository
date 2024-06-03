using Assignment_2.Repository;
using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Validations
{
    /// <summary>
    /// Represents a custom validation attribute for ensuring uniqueness of email addresses.
    /// </summary>
    public class UniqueEmailAttribute : ValidationAttribute
    {
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the UniqueEmailAttribute class.
        /// </summary>
        public UniqueEmailAttribute()
        {
            _userRepository = new UserRepository();
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A ValidationResult object that represents the result of the validation.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;

            if (string.IsNullOrWhiteSpace(email))
            {
                return ValidationResult.Success;
            }

            var existingUser = _userRepository.GetUserByEmail(email);

            if (existingUser != null)
            {
                return new ValidationResult("Email already in use. Please choose a different email.");
            }

            return ValidationResult.Success;
        }
    }
}

using Assignment_2.Repository;
using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Validations
{
    /// <summary>
    /// Represents a custom validation attribute for ensuring uniqueness of usernames.
    /// </summary>
    public class UsernameValidationAttribute : ValidationAttribute
    {
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the UsernameValidationAttribute class.
        /// </summary>
        public UsernameValidationAttribute()
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
            var username = value as string;

            if (string.IsNullOrWhiteSpace(username))
            {
                return ValidationResult.Success;
            }

            var existingUser = _userRepository.GetUserByUsername(username);

            if (existingUser != null)
            {
                return new ValidationResult("Username already in use. Please choose a different username.");
            }

            return ValidationResult.Success;
        }
    }
}

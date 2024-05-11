using Assignment_2.Repository;
using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Validations
{
    public class UsernameValidationAttribute : ValidationAttribute
    {
        private readonly UserRepository _userRepository;

        public UsernameValidationAttribute()
        {
            _userRepository = new UserRepository();
        }

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

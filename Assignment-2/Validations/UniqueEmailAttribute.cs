using Assignment_2.Repository;
using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Validations
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        private readonly UserRepository _userRepository;

        public UniqueEmailAttribute()
        {
            _userRepository = new UserRepository();
        }

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

using System.ComponentModel.DataAnnotations;

namespace Assessment_1.Validations
{
    public class UserTypeValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedValues = { "Rider", "Driver" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string stringValue && _allowedValues.Contains(stringValue, StringComparer.OrdinalIgnoreCase))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid user type. Allowed values are: {string.Join(", ", _allowedValues)}");
        }
    }
}

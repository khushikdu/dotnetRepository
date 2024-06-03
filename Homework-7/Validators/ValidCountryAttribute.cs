using System.ComponentModel.DataAnnotations;

namespace Homework_7.Validators
{
    /// <summary>
    /// Validation attribute for validating country names.
    /// </summary>
    public class ValidCountryAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the country name.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>Validation result indicating success or failure.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string country = value.ToString();

                string[] validCountries = { "USA", "Canada", "UK", "Australia" };

                if (Array.IndexOf(validCountries, country) == -1)
                {
                    return new ValidationResult("Invalid country. Valid countries are: USA, Canada, UK, Australia.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

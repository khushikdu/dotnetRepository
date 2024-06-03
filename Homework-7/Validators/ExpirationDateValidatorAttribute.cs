using System.ComponentModel.DataAnnotations;

namespace Homework_7.Validators
{
    /// <summary>
    /// Validation attribute for validating expiration dates.
    /// </summary>
    public class ExpirationDateValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the expiration date.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>Validation result indicating success or failure.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string expirationDate = value.ToString();

                if (!DateTime.TryParseExact(expirationDate, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result) ||
                    result < DateTime.Now)
                {
                    return new ValidationResult("Expiration date must be a future date in the format MM/YYYY.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

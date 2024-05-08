using System.ComponentModel.DataAnnotations;

namespace Homework_7.Validators
{
    /// <summary>
    /// Validation attribute for validating CVV (Card Verification Value) numbers.
    /// </summary>
    public class CVVValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the CVV number.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>Validation result indicating success or failure.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string cvv = value.ToString();

                if (cvv.Length < 3 || cvv.Length > 4 || !int.TryParse(cvv, out _))
                {
                    return new ValidationResult("CVV must be a 3 or 4-digit number.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

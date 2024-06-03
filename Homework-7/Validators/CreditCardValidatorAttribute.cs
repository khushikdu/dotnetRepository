using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Homework_7.Validators
{
    /// <summary>
    /// Validation attribute for validating credit card numbers.
    /// </summary>
    public class CreditCardValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the credit card number.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>Validation result indicating success or failure.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string creditCardNumber = value.ToString();

                creditCardNumber = Regex.Replace(creditCardNumber, "[^0-9]", "");

                if (creditCardNumber.Length < 13 || creditCardNumber.Length > 19)
                {
                    return new ValidationResult("Invalid credit card number format.");
                }
            }
            return ValidationResult.Success;
        }
    }
}

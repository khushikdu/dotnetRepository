using System.ComponentModel.DataAnnotations;

namespace Homework_7.Validators
{
    /// <summary>
    /// Validation attribute for validating gender values.
    /// </summary>
    public class ValidGenderAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the gender value.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>Validation result indicating success or failure.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string gender = value.ToString().ToLower();

                string[] validGenders = { "male", "female", "other" };

                if (Array.IndexOf(validGenders, gender) == -1)
                {
                    return new ValidationResult("Invalid gender. Valid genders are: Male, Female, Other.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

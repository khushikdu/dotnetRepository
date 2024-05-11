using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Assignment_2.Validations
{
    /// <summary>
    /// Represents a custom validation attribute for password validation.
    /// </summary>
    public class PasswordValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        public override bool IsValid(object value)
        {
            var password = value.ToString();

            var hasUpperCase = new Regex(@"[A-Z]+").IsMatch(password);

            var hasLowerCase = new Regex(@"[a-z]+").IsMatch(password);

            var hasDigit = new Regex(@"[0-9]+").IsMatch(password);

            var hasSpecialCharacter = new Regex(@"[!@#$%^&*()_+}{:;'?/><.,\|[\]~-]+").IsMatch(password);

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialCharacter;
        }
    }
}

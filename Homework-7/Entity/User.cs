using Homework_7.Validators;
using System.ComponentModel.DataAnnotations;

namespace Homework_7.Entity
{
    /// <summary>
    /// Represents a user entity.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmation password of the user.
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the age of the user.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the country of the user.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the credit card number of the user.
        /// </summary>
        public string? CreditCard { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the user's credit card.
        /// </summary>
        public string? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the CVV (Card Verification Value) of the user's credit card.
        /// </summary>
        public string? CVV { get; set; }
    }
}

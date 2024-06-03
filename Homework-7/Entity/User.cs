using Homework_7.Validators;
using System.ComponentModel.DataAnnotations;

namespace Homework_7.Entity
{
    /// <summary>
    /// Represents a user entity.
    /// </summary>
    public class User
    {
       
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public string? Country { get; set; }

        public string? Gender { get; set; }

        public string? CreditCard { get; set; }

        public string? ExpirationDate { get; set; }

        public string? CVV { get; set; }
    }
}

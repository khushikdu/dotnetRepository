using Assignment_2.Validations;
using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a model for user information.
/// </summary>
public class UserModel
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    [Required]
    [StringLength(50)]
    [UsernameValidation]
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(100)]
    [UniqueEmail(ErrorMessage = "Email already in use")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [Required]
    [MinLength(8)]
    [PasswordValidation]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    [StringLength(100)]
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    [Phone]
    [StringLength(20)]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    [Required(ErrorMessage = "Role is Required")]
    public string? Role { get; set; }
}

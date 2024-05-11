using Assignment_2.Validations;
using System;
using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Required]
    [StringLength(50)]
    [UsernameValidation]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    [UniqueEmail(ErrorMessage = "Email already in use")]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    [PasswordValidation]
    public string Password { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Address { get; set; }

    [Phone]
    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Role is Required")]
    public string? Role { get; set; }
}

using System.ComponentModel.DataAnnotations;

//Didn't change the nomenclature to ViewModel as the test cases were failing due to the previous namespace
namespace Assignment_2.DTO
{
    /// <summary>
    /// Represents the data transfer object (DTO) for user login information.
    /// </summary>
    public class LoginUser
    {
        
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}

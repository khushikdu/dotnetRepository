using Assignment_2.CustomExceptions;
using Assignment_2.DTO;
using Assignment_2.Repository;
using Assignment_2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment_2.Controllers
{
    /// <summary>
    /// Controller for user authentication and registration.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private readonly UserService _userService;

        /// <summary>
        /// Constructor for AuthController.
        /// </summary>
        /// <param name="config">An instance of IConfiguration for retrieving configuration settings.</param>
        /// <param name="userService">An instance of UserService for user-related operations.</param>
        public AuthController(IConfiguration config, UserService userService)
        {
            _userService = userService;
            _config = config;
        }

        /// <summary>
        /// Endpoint for user authentication.
        /// </summary>
        /// <param name="loginRequest">The login credentials provided by the user.</param>
        /// <returns>Returns a JWT token upon successful authentication.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] LoginUser loginRequest)
        {
            try
            {
                // Authenticate user
                var user = _userService.GetUserByUsername(loginRequest.Username);

                if (user == null)
                {
                    throw new InvalidCredentialsException("Invalid username or password");
                }

                if (user.Password != loginRequest.Password)
                {
                    throw new InvalidCredentialsException("Incorrect password");
                }

                // Generate JWT token
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(jwtToken);
            }
            catch (GlobalException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for user registration.
        /// </summary>
        /// <param name="userDto">The user information provided for registration.</param>
        /// <returns>Returns HTTP status codes indicating the result of the registration attempt.</returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _userService.RegisterUser(userDto);

                if (result == "User registered successfully")
                {
                    return Ok(result);
                }
                else
                {
                    return Conflict(result);
                }
            }
            catch (GlobalException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

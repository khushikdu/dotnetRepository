using Assignment_2.CustomExceptions;
using Assignment_2.DTO;
using Assignment_2.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment_2.Controllers
{
    /// <summary>
    /// Endpoint for user authentication.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor for AuthController.
        /// </summary>
        /// <param name="config">Configuration object.</param>
        /// <param name="userService">User service instance.</param>
        /// <param name="authService">Authentication service instance.</param>
        public AuthController(IConfiguration config, IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _config = config;
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user based on login credentials.
        /// </summary>
        /// <param name="loginRequest">Login credentials provided by the user.</param>
        /// <returns>JWT token upon successful authentication.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] LoginUser loginRequest)
        {
            var token = _authService.Authenticate(loginRequest);
            return Ok(token);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userDto">User details for registration.</param>
        /// <returns>Result of the registration process.</returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO userDto)
        {
            var result = _userService.RegisterUser(userDto);
            return Ok(result);
        }
    }
}

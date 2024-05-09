using Homework_7.Entity;
using Homework_7.Repository;
using Microsoft.AspNetCore.Mvc;
using Homework_7.DTO;
using Homework_7.Mapper;
using Microsoft.Extensions.Logging; 

namespace Homework_7.Controllers
{
    [ApiController]
    [Route("api/register")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserRegistrationRepository _userRegistrationService;
        private readonly ILogger<RegistrationController> _logger;

        /// <summary>
        /// Constructor for RegistrationController.
        /// </summary>
        /// <param name="userRegistrationService">User registration service.</param>
        /// <param name="logger">Logger instance for logging.</param>
        public RegistrationController(IUserRegistrationRepository userRegistrationService, ILogger<RegistrationController> logger)
        {
            _userRegistrationService = userRegistrationService;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint to register a new user.
        /// </summary>
        /// <param name="userDto">DTO object containing user registration details.</param>
        /// <returns>HTTP status code indicating success or failure of the registration process.</returns>
        [HttpPost]
        public IActionResult Register([FromBody] UserDTO userDto)
        {
            UserMapper userMapper = new UserMapper();
            User user = userMapper.Map(userDto);
            _userRegistrationService.Save(user);
            _logger.LogInformation("User registered successfully: {Username}", user.Username);
            return Ok("User registered successfully");
        }

        /// <summary>
        /// Retrieves all registered users.
        /// </summary>
        /// <returns>A list of UserDTO objects representing the registered users.</returns>
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            List<UserDTO> users = _userRegistrationService.GetAllUsers();
            return Ok(users);
        }

    }
}

using Homework_7.Entity;
using Microsoft.AspNetCore.Mvc;
using Homework_7.Mapper;
using Microsoft.Extensions.Logging;
using Homework_7.Service.Interface;
using Homework_7.ViewModel;

namespace Homework_7.Controllers
{
    [ApiController]
    [Route("api/register")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(IUserRegistrationService userRegistrationService, ILogger<RegistrationController> logger)
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
        public IActionResult Register([FromBody] UserVM user)
        {
            _userRegistrationService.Save(user);
            return Ok("User registered successfully");
        }

        /// <summary>
        /// Retrieves all registered users.
        /// </summary>
        /// <returns>A list of UserDTO objects representing the registered users.</returns>
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            List<UserVM> users = _userRegistrationService.GetAllUsers();
            return Ok(users);
        }

    }
}

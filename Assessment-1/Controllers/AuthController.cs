using Assessment_1.Enums;
using Assessment_1.Interfaces.IService;
using Assessment_1.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        /// <summary>
        /// Logs in a user and returns a JWT token.
        /// </summary>
        /// <param name="userLogin">The login request containing the user's email and password.</param>
        /// <returns>An IActionResult containing the JWT token if successful, otherwise an Unauthorized result.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            string token = _authorizeService.Login(userLogin);
            if (token == "Invalid Email/Phone or Password.")
            {
                return Unauthorized(new { Message = token });
            }
            return Ok(token);
            //return Ok(new { Token = token });
        }

        /// <summary>
        /// Signs up a new driver.
        /// </summary>
        /// <param name="addDriver">The signup request containing the driver's details.</param>
        /// <returns>An IActionResult indicating success or conflict if the driver already exists.</returns>
        [HttpPost("driverSignup")]
        public IActionResult SignUp([FromBody] AddDriver addDriver)
        {
            addDriver.UserType = UserType.Driver;
            string result = _authorizeService.AddDriver(addDriver);
            if (result.Contains("already exists"))
            {
                return Conflict(new { Message = result });
            }

            return Ok(new { Message = result });
        }

        /// <summary>
        /// Signs up a new rider.
        /// </summary>
        /// <param name="addUser">The signup request containing the rider's details.</param>
        /// <returns>An IActionResult indicating success or conflict if the rider already exists.</returns>
        [HttpPost("riderSignup")]
        public IActionResult SignUp([FromBody] AddUser addUser)
        {
            addUser.UserType = UserType.Rider;
            string result = _authorizeService.AddRider(addUser);
            if (result.Contains("already exists"))
            {
                return Conflict(new { Message = result });
            }

            return Ok(new { Message = result });
        }
    }
}

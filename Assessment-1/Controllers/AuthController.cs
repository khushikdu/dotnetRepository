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

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var token = _authorizeService.Login(userLogin);
            if (token == "Invalid Username or Password.")
            {
                return Unauthorized(new { Message = token });
            }
            return Ok(token);
            //return Ok(new { Token = token });
        }

        [HttpPost("driverSignup")]
        public IActionResult SignUp([FromBody] AddDriver addDriver)
        {
            addDriver.UserType = UserType.Driver;
            var result = _authorizeService.AddDriver(addDriver);
            if (result.Contains("already exists"))
            {
                return Conflict(new { Message = result });
            }

            return Ok(new { Message = result });
        }

        [HttpPost("riderSignup")]
        public IActionResult SignUp([FromBody] AddUser addUser)
        {
            addUser.UserType = UserType.Rider;
            var result = _authorizeService.AddRider(addUser);
            if (result.Contains("already exists"))
            {
                return Conflict(new { Message = result });
            }

            return Ok(new { Message = result });
        }
    }
}

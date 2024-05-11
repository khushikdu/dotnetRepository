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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private readonly UserService _userService;

        public AuthController(IConfiguration config, UserService userService)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginUser loginRequest)
        {
            try
            {

                var user = _userService.GetUserByUsername(loginRequest.Username);

                if (user == null)
                {
                    throw new InvalidCredentialsException("Invalid username or password");
                }

                if (user.Password != loginRequest.Password)
                {
                    throw new InvalidCredentialsException("Incorrect password");
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

                var Sectoken = new JwtSecurityToken(
                  _config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                return Ok(token);
            }
            catch(GlobalException ex) 
            {
                return Unauthorized(ex.Message);
            }

        }

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

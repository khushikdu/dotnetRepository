using Assignment_2.Repository;
using Assignment_2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assignment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("get-user")]
        public IActionResult GetAuthenticated()
        {
            // Get the claims associated with the authenticated user
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usernameClaim = claimsIdentity.FindFirst(ClaimTypes.Name);

            if (usernameClaim != null)
            {
                var username = usernameClaim.Value;

                // Retrieve user details from the repository based on the username
                var user = _userService.GetUserByUsername(username);

                if (user != null)
                {
                    // Return user details
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            else
            {
                // Handle the case where the username claim is not found
                return BadRequest("Username claim not found.");
            }
        }
        
    }
}

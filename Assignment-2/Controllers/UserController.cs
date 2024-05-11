using Assignment_2.Repository;
using Assignment_2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assignment_2.Controllers
{
    /// <summary>
    /// Controller for retrieving user details.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        /// <summary>
        /// Constructor for UserController.
        /// </summary>
        /// <param name="userService">An instance of UserService for user-related operations.</param>
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Endpoint for retrieving user details.
        /// </summary>
        /// <returns>Returns user details for the authenticated user.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("get-user")]
        public IActionResult GetAuthenticated()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usernameClaim = claimsIdentity.FindFirst(ClaimTypes.Name);

            if (usernameClaim != null)
            {
                var username = usernameClaim.Value;

                var user = _userService.GetUserByUsername(username);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            else
            {
                return BadRequest("Username claim not found.");
            }
        }
    }
}

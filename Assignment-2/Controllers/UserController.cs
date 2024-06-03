using Assignment_2.Repository;
using Assignment_2.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assignment_2.Controllers
{
    /// <summary>
    /// Controller for user-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for UserController.
        /// </summary>
        /// <param name="userService">User service instance.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves details of the authenticated user.
        /// </summary>
        /// <returns>Details of the authenticated user.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("get-user")]
        public IActionResult GetAuthenticated()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usernameClaim = claimsIdentity.FindFirst(ClaimTypes.Name);

            var username = usernameClaim.Value;

            var user = _userService.GetUserByUsername(username);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound("User not found");
        }
    }
}

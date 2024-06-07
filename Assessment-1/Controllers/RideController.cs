using Assessment_1.Enums;
using Assessment_1.Interfaces.IService;
using Assessment_1.Models.Request;
using Assessment_1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Rider,Driver")]
    public class RideController : ControllerBase
    {
        private readonly IRideService _rideService;

        public RideController(IRideService rideService)
        {
            _rideService = rideService;
        }

        /// <summary>
        /// Cancels a ride.
        /// </summary>
        /// <param name="cancelRideRequest">The request containing the ride ID to be canceled.</param>
        /// <returns>An IActionResult indicating the result of the cancel operation.</returns>
        [HttpPost("cancel-ride")]
        public IActionResult CancelRide([FromBody] CancelOrEndRideRequest cancelRideRequest)
        {
            string userEmail = JwtUtils.GetEmailFromClaims(User);
            string userRole = JwtUtils.GetRoleFromClaims(User);
            UserType userType = userRole == "Rider" ? UserType.Rider : UserType.Driver;

            try
            {
                _rideService.CancelRide(cancelRideRequest.RideId, userEmail, userType);
                return Ok(new { Message = "Ride canceled successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Ends a ride.
        /// </summary>
        /// <param name="endRideRequest">The request containing the ride ID to be ended.</param>
        /// <returns>An IActionResult indicating the result of the end operation.</returns>
        [HttpPost("end-ride")]
        public IActionResult EndRide([FromBody] CancelOrEndRideRequest endRideRequest)
        {
            string userEmail = JwtUtils.GetEmailFromClaims(User);
            string userRole = JwtUtils.GetRoleFromClaims(User);
            UserType userType = userRole == "Rider" ? UserType.Rider : UserType.Driver;

            try
            {
                _rideService.EndRide(endRideRequest.RideId, userEmail, userType);
                return Ok(new { Message = "Ride ended successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

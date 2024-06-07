using Assessment_1.Enums;
using Assessment_1.Interfaces.IService;
using Assessment_1.Models.Request;
using Assessment_1.Models.Response;
using Assessment_1.Services;
using Assessment_1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Driver")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        /// <summary>
        /// Toggles the availability of the driver.
        /// </summary>
        /// <returns>An IActionResult indicating the result of the availability toggle.</returns>
        [HttpPut("toggle-availability")]
        public IActionResult ToggleAvailability()
        {
            string driverEmail = JwtUtils.GetEmailFromClaims(User);
            _driverService.ToggleAvailability(driverEmail);

            return Ok("");
        }

        /// <summary>
        /// Gets the current ride details for the driver.
        /// </summary>
        /// <returns>An IActionResult containing the current ride details or a message if no ongoing ride is found.</returns>
        [HttpGet("current-ride")]
        public IActionResult GetCurrentRide()
        {
            string driverEmail = JwtUtils.GetEmailFromClaims(User);
            try
            {
                RideResponseDriver response = _driverService.GetCurrentRide(driverEmail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Starts a ride for the driver.
        /// </summary>
        /// <param name="startRideRequest">The request containing ride ID and OTP.</param>
        /// <returns>An IActionResult containing the start ride response or an error message if the ride could not be started.</returns>
        [HttpPost("start-ride")]
        public IActionResult StartRide([FromBody] StartRideRequest startRideRequest)
        {
            string driverEmail = JwtUtils.GetEmailFromClaims(User);
            try
            {
                StartRideResponse response = _driverService.StartRide(startRideRequest, driverEmail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Rates a rider for a completed ride.
        /// </summary>
        /// <param name="rateRiderRequest">The request containing ride ID and rating value.</param>
        /// <returns>An IActionResult indicating the result of the rating operation.</returns>
        [HttpPost("rate-rider")]
        public IActionResult RateRider([FromBody] RateRequest rateRiderRequest)
        {
            string driverEmail = JwtUtils.GetEmailFromClaims(User);

            try
            {
                _driverService.RateRider(rateRiderRequest, driverEmail);
                return Ok(new { Message = "Rider rated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

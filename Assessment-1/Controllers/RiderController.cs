using Assessment_1.Enums;
using Assessment_1.Interfaces.IService;
using Assessment_1.Models.Request;
using Assessment_1.Models.Response;
using Assessment_1.Services;
using Assessment_1.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assessment_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Rider")]
    public class RiderController : ControllerBase
    {
        private readonly IRiderService _riderService;

        public RiderController(IRiderService riderService)
        {
            _riderService = riderService;
        }

        /// <summary>
        /// Requests a ride for the rider.
        /// </summary>
        /// <param name="rideRequest">The request containing ride details.</param>
        /// <returns>An IActionResult containing the ride response or an error message.</returns>
        [HttpPost("request-ride")]
        public IActionResult RequestRide([FromBody] RideRequest rideRequest)        
        {
            string riderEmail = JwtUtils.GetEmailFromClaims(User);
            try
            {
                RideResponseRider? response = _riderService.RequestRide(riderEmail, rideRequest);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                return Ok(ex.Message.ToString());                
            }        
        }

        /// <summary>
        /// Gets the details of the current ride for the rider.
        /// </summary>
        /// <returns>An IActionResult containing the current ride details or an error message.</returns>
        [HttpGet("current-ride")]
        public IActionResult GetCurrentRide()
        {
            string riderEmail = JwtUtils.GetEmailFromClaims(User);

            try
            {
                RideResponseRider? response = _riderService.GetCurrentRide(riderEmail);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                return Ok(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Rates the driver for a completed ride.
        /// </summary>
        /// <param name="rateDriverRequest">The request containing the ride ID and rating value.</param>
        /// <returns>An IActionResult indicating the result of the rating operation.</returns>
        [HttpPost("rate-driver")]
        public IActionResult RateDriver([FromBody] RateRequest rateDriverRequest)
        {
            string riderEmail = JwtUtils.GetEmailFromClaims(User);

            try
            {
                _riderService.RateDriver(rateDriverRequest, riderEmail);
                return Ok(new { Message = "Driver rated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

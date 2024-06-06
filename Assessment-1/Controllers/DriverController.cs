using Assessment_1.Enums;
using Assessment_1.Interfaces.IService;
using Assessment_1.Models.Request;
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

        [HttpPut("toggle-availability")]
        public IActionResult ToggleAvailability()
        {
            var driverEmail = JwtUtils.GetEmailFromClaims(User);
            _driverService.ToggleAvailability(driverEmail);

            return Ok("");
        }

        [HttpGet("current-ride")]
        public IActionResult GetCurrentRide()
        {
            var driverEmail = JwtUtils.GetEmailFromClaims(User);
            try
            {
                var response = _driverService.GetCurrentRide(driverEmail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }

        [HttpPost("start-ride")]
        public IActionResult StartRide([FromBody] StartRideRequest startRideRequest)
        {
            var driverEmail = JwtUtils.GetEmailFromClaims(User);
            try
            {
                var response = _driverService.StartRide(startRideRequest, driverEmail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("rate-rider")]
        public IActionResult RateRider([FromBody] RateRequest rateRiderRequest)
        {
            var driverEmail = JwtUtils.GetEmailFromClaims(User);

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

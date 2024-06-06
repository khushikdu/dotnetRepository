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

        [HttpPost("request-ride")]
        public IActionResult RequestRide([FromBody] RideRequest rideRequest)        
        {
            var riderEmail = JwtUtils.GetEmailFromClaims(User);
            try
            {
                var response = _riderService.RequestRide(riderEmail, rideRequest);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                return Ok(ex.Message.ToString());                
            }        
        }

        [HttpGet("current-ride")]
        public IActionResult GetCurrentRide()
        {
            var riderEmail = JwtUtils.GetEmailFromClaims(User);

            try
            {
                var response = _riderService.GetCurrentRide(riderEmail);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                return Ok(ex.Message.ToString());
            }
        }
        [HttpPost("rate-driver")]
        public IActionResult RateDriver([FromBody] RateRequest rateDriverRequest)
        {
            var riderEmail = JwtUtils.GetEmailFromClaims(User);

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

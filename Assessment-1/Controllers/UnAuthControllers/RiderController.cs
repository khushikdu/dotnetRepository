using Assessment_1.Interfaces.IService;
using Assessment_1.ViewModel.RequestVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assessment_1.Controllers.UnAuthControllers { 

    [ApiController]
    [Route("api/[controller]")]
    public class RiderController : Controller
    {
        private readonly IRiderService _riderService;


    public RiderController(IRiderService riderService)
    {
        _riderService = riderService;
    }

    [HttpPost]
        public ActionResult<int> AddRider(AddRiderVM riderVM)
        {
            int customerId = _riderService.AddRider(riderVM);
            return Ok(customerId);
        }

        [Authorize]
        [HttpGet("bookCab")]
        public IActionResult GetAuthenticated()
        {
            return Ok("Hello World");
        }

        [HttpPost("book-ride")]
        public ActionResult<int> BookRide([FromBody] RequestRideVM requestRide)
        {
            
                int rideId = _riderService.BookRide(requestRide, 1);
                return Ok(rideId);
        }
    }

}

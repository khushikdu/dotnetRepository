using Assessment_1.Interfaces.IService;
using Assessment_1.ViewModel.RequestVM;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPost]
        public ActionResult<int> AddDriver(AddDriverVM driverVM)
        {
            int driverId = _driverService.AddDriver(driverVM);
            return Ok(driverId);
      
        }
        [HttpPost("startRide")]
        public ActionResult<int> StartRide(int driverId, int otp)
        {
            //passing 1 as ride id and driverid
            _driverService.StartRide(1,1,otp);
            
            return Ok("Ride Started");

        }

    }
}

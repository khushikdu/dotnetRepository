using Assessment_1.Entity;
using Assessment_1.Interfaces.IRepository;
using Assessment_1.Interfaces.IService;
using Assessment_1.ViewModel.RequestVM;

namespace Assessment_1.Service
{
    public class RiderService : IRiderService
    {
        private readonly IRiderRepository _riderRepository;
        private readonly IDriverService _driverService;


        public RiderService(IRiderRepository riderRepository, IDriverService driverService)
        {
            _driverService = driverService;
            
            _riderRepository = riderRepository;
        }
        public int AddRider(AddRiderVM addRider)
        {
            return _riderRepository.AddRider(addRider);
        }
        public int BookRide(RequestRideVM requestRide, int riderId)
        {
            var availableDrivers = _driverService.GetDriversByVehicleType(requestRide.Vehicle);
            if (availableDrivers == null || !availableDrivers.Any())
            {
                throw new InvalidOperationException("No available driver found.");
            }

            var driver = availableDrivers.First();
            var ride = new Ride
            {
                RiderId = riderId,
                DriverId = driver.DriverId,
                Pickup = requestRide.PickUp,
                Drop = requestRide.Drop,
                Status = "NotStarted",
                Date = DateTime.Now,
                StartTime = null,
                EndTime = null
            };

            driver.Status = "Unavailable";
            //_driverService.UpdateDriverStatus(driver);

            return _riderRepository.BookRide(ride);
        }

    }
}

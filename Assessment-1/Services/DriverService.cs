using Assessment_1.DBContext;
using Assessment_1.Enums;
using Assessment_1.Interfaces.IService;
using Assessment_1.Models.Request;
using Assessment_1.Models.Response;
using Assessment_1.Constants;
using System;
using System.Linq;
using Assessment_1.Utilities;

namespace Assessment_1.Services
{
    public class DriverService : IDriverService
    {
        private readonly TaxiService _context;

        public DriverService(TaxiService context)
        {
            _context = context;
        }

        public void ToggleAvailability(string driverEmail)
        {
            var driver = _context.Users.FirstOrDefault(u => u.Email == driverEmail && u.UserType == UserType.Driver);
            if (driver == null)
            {
                throw new Exception(ErrorMessages.UserNotDriver);
            }

            var vehicleAvailability = _context.VehiclesAndAvailability.FirstOrDefault(v => v.UserId == driver.UserId);
            if (vehicleAvailability == null)
            {
                throw new Exception(ErrorMessages.NoVehicleAssigned);
            }

            bool hasOngoingOrNotStartedRide = _context.Rides.Any(r => r.DriverId == driver.UserId &&(r.Status==RideStatus.Ongoing|| r.Status==RideStatus.NotStarted));

            if (hasOngoingOrNotStartedRide) 
            {
                throw new Exception(ErrorMessages.CannotToggeState);
            }

            vehicleAvailability.IsAvailable = !vehicleAvailability.IsAvailable;
            _context.SaveChanges();
        }

        public RideResponseDriver GetCurrentRide(string driverEmail)
        {
            var driver = _context.Users.FirstOrDefault(u => u.Email == driverEmail && u.UserType == UserType.Driver);
            if (driver == null)
            {
                throw new Exception(ErrorMessages.DriverNotFound);
            }

            var ongoingRide = _context.Rides.FirstOrDefault(r => r.DriverId == driver.UserId && r.Status == RideStatus.NotStarted);
            if (ongoingRide == null)
            {
                throw new Exception(ErrorMessages.NoOngoingRideAvailable);
            }

            var rider = _context.Users.FirstOrDefault(u => u.UserId == ongoingRide.RiderId);
            if (rider == null)
            {
                throw new Exception(ErrorMessages.RiderNotFound);
            }

            return new RideResponseDriver
            {
                RiderName = rider.Name,
                RideStatus = ongoingRide.Status.ToString(),
                PickupLocation = ongoingRide.PickupLocation,
                DropLocation = ongoingRide.DropLocation
            };
        }
        public StartRideResponse StartRide(StartRideRequest startRideRequest, string driverEmail)
        {
            var driver = _context.Users.FirstOrDefault(u => u.Email == driverEmail && u.UserType == UserType.Driver);
            if (driver == null)
            {
                throw new Exception(ErrorMessages.UserNotDriver);
            }

            var ride = _context.Rides.FirstOrDefault(r => r.Id == startRideRequest.RideId);
            
            if (ride == null||ride.DriverId != driver.UserId)
            {
                throw new Exception(ErrorMessages.RideNotFound);
            }

            if (ride.OTP != startRideRequest.OTP)
            {
                throw new Exception(ErrorMessages.InvalidOTP);
            }

            if (ride.Status != RideStatus.NotStarted)
            {
                throw new Exception(ErrorMessages.RideNotInNotStartedStatus);
            }
            ride.StartedAt=DateTime.Now;
            ride.Status = RideStatus.Ongoing;
            _context.SaveChanges();

            return new StartRideResponse
            {
                RideId = ride.Id,
                Message = "Ride started successfully.",
                RideStatus = ride.Status.ToString()
            };
        }
        public void RateRider(RateRequest rateRiderRequest, string driverEmail)
        {
            var ride = _context.Rides.FirstOrDefault(r => r.Id.ToString() == rateRiderRequest.RideId);

            if (ride == null)
            {
                throw new Exception(ErrorMessages.RideNotFound);
            }

            var driver = _context.Users.FirstOrDefault(u => u.Email == driverEmail);
            if (ride.DriverId != driver.UserId)
            {
                throw new Exception(ErrorMessages.RideNotFound);
            }

            if (ride.Status != RideStatus.Completed)
            {
                throw new Exception(ErrorMessages.CannotRateRideNotEnded);
            }

            if (rateRiderRequest.Rating < 1 || rateRiderRequest.Rating > 5)
            {
                throw new ArgumentException(ErrorMessages.InvalidRating);
            }

            var existingRating = _context.Ratings.FirstOrDefault(r => r.RideId == ride.Id && r.RatedBy == UserType.Driver);
            if (existingRating != null)
            {
                throw new Exception(ErrorMessages.AlreadyRatedRider);
            }

            var rating = RatingsMapper.MapToRatings(ride.Id, rateRiderRequest.Rating, UserType.Driver);

            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }
    }
}

using Assessment_1.Interfaces.IService;
using Assessment_1.Models.Request;
using Assessment_1.Models.Response;
using Assessment_1.DBContext;
using Assessment_1.Entitites;
using System;
using System.Linq;
using Assessment_1.Enums;
using Assessment_1.Utils;
using Assessment_1.Utilities;
using Assessment_1.Constants;

namespace Assessment_1.Services
{
    public class RiderService : IRiderService
    {
        private readonly TaxiService _context;

        public RiderService(TaxiService context)
        {
            _context = context;
        }

        public RideResponseRider RequestRide(string riderEmail, RideRequest rideRequest)
        {
            // to check if the rider exists
            var rider = _context.Users.FirstOrDefault(u => u.Email == riderEmail && u.UserType == UserType.Rider);

            var riderAsDriver = _context.Users.FirstOrDefault(u => u.Email == riderEmail && u.UserType == UserType.Driver);

            var vehicleType = rideRequest.TypeOfRide.ToLower() switch
            {
                "bike" => VehicleType.Bike,
                "car" => VehicleType.Car,
                "auto" => VehicleType.Auto,
            };

            if (rider == null)
            {
                throw new Exception(ErrorMessages.UserNotRider);
            }

            //to check if he has an ongoing/pending ride
            var ongoingOrPendingRide = _context.Rides.FirstOrDefault(r => r.RiderId == rider.UserId && (r.Status == RideStatus.Ongoing || r.Status == RideStatus.NotStarted));
            if (ongoingOrPendingRide != null)
            {
                
                throw new Exception(ErrorMessages.RideNotAllowed);
            }

            //to get a ride 
            VehicleAndAvailability? availableVehicle = null;
            if (riderAsDriver != null)
            {
                availableVehicle = _context.VehiclesAndAvailability.FirstOrDefault(v => v.IsAvailable && v.VehicleType==vehicleType && v.UserId != riderAsDriver.UserId);
            } 
            else 
            {
                availableVehicle = _context.VehiclesAndAvailability.FirstOrDefault(v => v.IsAvailable && v.VehicleType == vehicleType);               
            }

            if (availableVehicle == null)
            {
                throw new Exception(ErrorMessages.NoAvailableDriver);
            }

            //to get the details of the assigned driver
            var assignedDriver = _context.Users.FirstOrDefault(u => (u.UserId == availableVehicle.UserId));

            availableVehicle.IsAvailable = false;
            _context.SaveChanges();

            string otp = GenerateOTP.GenOTP();

            //var ride = RIde
            
            var ride = new Ride
            {
                Id = Guid.NewGuid(),
                RiderId = rider.UserId,
                DriverId = assignedDriver.UserId,
                PickupLocation = rideRequest.PickupLocation,
                DropLocation = rideRequest.DropLocation,
                TypeOfRide = vehicleType, 
                OTP = otp,
                Status = RideStatus.NotStarted
            };
            _context.Rides.Add(ride);
            _context.SaveChanges();

            var response = new RideResponseRider
            {
                RideId = ride.Id,
                DriverName = assignedDriver.Name,
                DriverPhone = assignedDriver.Phone,
                OTP = otp,
                RideStatus = ride.Status.ToString()
            };

            return response;
        }
        public RideResponseRider GetCurrentRide(string riderEmail)
        {
            var rider = _context.Users.FirstOrDefault(u => u.Email == riderEmail && u.UserType == UserType.Rider);
            if (rider == null)
            {
                throw new Exception(ErrorMessages.RiderNotFound);
            }

            var ongoingRide = _context.Rides
                .FirstOrDefault(r => r.RiderId == rider.UserId && (r.Status == RideStatus.NotStarted || r.Status == RideStatus.Ongoing));

            if (ongoingRide == null)
            {
                throw new Exception(ErrorMessages.NoOngoingRideAvailable);
            }

            var driver = _context.Users.FirstOrDefault(u => u.UserId == ongoingRide.DriverId);

            var response = new RideResponseRider
            {
                RideId = ongoingRide.Id,
                DriverName = driver.Name,
                DriverPhone = driver.Phone,
                OTP = ongoingRide.Status == RideStatus.NotStarted ? ongoingRide.OTP : null,
                RideStatus = ongoingRide.Status.ToString()
            };

            return response;
        }

        public void RateDriver(RateRequest rateDriverRequest, string riderEmail)
        {
            var ride = _context.Rides.FirstOrDefault(r => r.Id.ToString() == rateDriverRequest.RideId);

            if (ride == null)
            {
                throw new Exception(ErrorMessages.RideNotFound);
            }

            var rider = _context.Users.FirstOrDefault(u => u.Email == riderEmail);
            if (ride.RiderId != rider.UserId)
            {
                throw new Exception(ErrorMessages.RideNotFound);
            }

            if (ride.Status != RideStatus.Completed)
            {
                throw new Exception(ErrorMessages.CannotRateRideNotEnded);
            }

            if (rateDriverRequest.Rating < 1 || rateDriverRequest.Rating > 5)
            {
                throw new ArgumentException(ErrorMessages.InvalidOTP);
            }

            var existingRating = _context.Ratings.FirstOrDefault(r => r.RideId == ride.Id && r.RatedBy == UserType.Rider);
            if (existingRating != null)
            {
                throw new Exception(ErrorMessages.AlreadyRatedDriver);
            }

            var rating = RatingsMapper.MapToRatings(ride.Id, rateDriverRequest.Rating, UserType.Rider);

            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }
    }
}

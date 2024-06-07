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
using Assessment_1.Exceptions;

namespace Assessment_1.Services
{
    public class RiderService : IRiderService
    {
        private readonly TaxiService _context;

        public RiderService(TaxiService context)
        {
            _context = context;
        }

        /// <summary>
        /// Requests a ride for a rider.
        /// </summary>
        /// <param name="riderEmail">The email of the rider.</param>
        /// <param name="rideRequest">The ride request details.</param>
        /// <returns>The response containing ride details.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the rider is not found.</exception>
        /// <exception cref="OngoingOrPendingRideException">Thrown when the rider has an ongoing or pending ride.</exception>
        /// <exception cref="RideNotFoundException">Thrown when no available driver is found.</exception>
        public RideResponseRider RequestRide(string riderEmail, RideRequest rideRequest)
        {
            // to check if the rider exists
            User? rider = GetRider(riderEmail);

            User? riderAsDriver = _context.Users.FirstOrDefault(u => u.Email == riderEmail && u.UserType == UserType.Driver);

            VehicleType vehicleType = rideRequest.TypeOfRide.ToLower() switch
            {
                "bike" => VehicleType.Bike,
                "car" => VehicleType.Car,
                "auto" => VehicleType.Auto,
            };

            if (rider == null)
            {
                throw new UserNotFoundException(ErrorMessages.UserNotRider);
            }

            //to check if he has an ongoing/pending ride
            Ride? ongoingOrPendingRide = _context.Rides.FirstOrDefault(r => r.RiderId == rider.UserId && (r.Status == RideStatus.Ongoing || r.Status == RideStatus.NotStarted));
            if (ongoingOrPendingRide != null)
            {
                
                throw new OngoingOrPendingRideException(ErrorMessages.RideNotAllowed);
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
                throw new RideNotFoundException(ErrorMessages.NoAvailableDriver);
            }

            //to get the details of the assigned driver
            User assignedDriver = _context.Users.FirstOrDefault(u => (u.UserId == availableVehicle.UserId));

            availableVehicle.IsAvailable = false;
            _context.SaveChanges();

            string otp = GenerateOTP.GenOTP();
            
            Ride ride = new Ride
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

            RideResponseRider response = new RideResponseRider
            {
                RideId = ride.Id,
                DriverName = assignedDriver.Name,
                DriverPhone = assignedDriver.Phone,
                OTP = otp,
                RideStatus = ride.Status.ToString()
            };

            return response;
        }
        /// <summary>
        /// Gets the current ride of a rider.
        /// </summary>
        /// <param name="riderEmail">The email of the rider.</param>
        /// <returns>The current ride details.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the rider is not found.</exception>
        /// <exception cref="OngoingOrPendingRideException">Thrown when there is no ongoing ride available.</exception>
        public RideResponseRider GetCurrentRide(string riderEmail)
        {
            User? rider = GetRider(riderEmail);
            if (rider == null)
            {
                throw new UserNotFoundException(ErrorMessages.RiderNotFound);
            }

            Ride? ongoingRide = _context.Rides
                .FirstOrDefault(r => r.RiderId == rider.UserId && (r.Status == RideStatus.NotStarted || r.Status == RideStatus.Ongoing));

            if (ongoingRide == null)
            {
                throw new OngoingOrPendingRideException(ErrorMessages.NoOngoingRideAvailable);
            }

            User driver = _context.Users.FirstOrDefault(u => u.UserId == ongoingRide.DriverId);

            RideResponseRider response = new RideResponseRider
            {
                RideId = ongoingRide.Id,
                DriverName = driver.Name,
                DriverPhone = driver.Phone,
                OTP = ongoingRide.Status == RideStatus.NotStarted ? ongoingRide.OTP : null,
                RideStatus = ongoingRide.Status.ToString()
            };

            return response;
        }

        /// <summary>
        /// Rates a driver.
        /// </summary>
        /// <param name="rateDriverRequest">The request containing rating details.</param>
        /// <param name="riderEmail">The email of the rider.</param>
        /// <exception cref="RideNotFoundException">Thrown when the ride is not found.</exception>
        /// <exception cref="OngoingOrPendingRideException">Thrown when the ride is not completed.</exception>
        /// <exception cref="ArgumentException">Thrown when the rating is invalid.</exception>
        /// <exception cref="AlreadyRatedException">Thrown when the driver has already been rated.</exception>
        public void RateDriver(RateRequest rateDriverRequest, string riderEmail)
        {
            Ride? ride = _context.Rides.FirstOrDefault(r => r.Id.ToString() == rateDriverRequest.RideId);

            if (ride == null)
            {
                throw new RideNotFoundException(ErrorMessages.RideNotFound);
            }

            User rider = _context.Users.FirstOrDefault(u => u.Email == riderEmail);
            if (ride.RiderId != rider.UserId)
            {
                throw new RideNotFoundException(ErrorMessages.RideNotFound);
            }

            if (ride.Status != RideStatus.Completed)
            {
                throw new OngoingOrPendingRideException(ErrorMessages.CannotRateRideNotEnded);
            }

            if (rateDriverRequest.Rating < 1 || rateDriverRequest.Rating > 5)
            {
                throw new ArgumentException(ErrorMessages.InvalidOTP);
            }

            Ratings? existingRating = _context.Ratings.FirstOrDefault(r => r.RideId == ride.Id && r.RatedBy == UserType.Rider);
            if (existingRating != null)
            {
                throw new AlreadyRatedException(ErrorMessages.AlreadyRatedDriver);
            }

            Ratings rating = RatingsMapper.MapToRatings(ride.Id, rateDriverRequest.Rating, UserType.Rider);

            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }
        /// <summary>
        /// Gets a rider by email.
        /// </summary>
        /// <param name="riderEmail">The email of the rider.</param>
        /// <returns>The rider entity.</returns>
        private User? GetRider(string riderEmail)
        {
            return _context.Users.FirstOrDefault(u => u.Email == riderEmail && u.UserType == UserType.Rider);
        }
    }
}

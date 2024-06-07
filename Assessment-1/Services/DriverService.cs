using Assessment_1.DBContext;
using Assessment_1.Enums;
using Assessment_1.Interfaces.IService;
using Assessment_1.Models.Request;
using Assessment_1.Models.Response;
using Assessment_1.Constants;
using System;
using System.Linq;
using Assessment_1.Utilities;
using Assessment_1.Exceptions;
using Assessment_1.Entitites;

namespace Assessment_1.Services
{
    public class DriverService : IDriverService
    {
        private readonly TaxiService _context;

        public DriverService(TaxiService context)
        {
            _context = context;
        }

        /// <summary>
        /// Toggles the availability of a driver.
        /// </summary>
        /// <param name="driverEmail">The email of the driver.</param>
        /// <exception cref="UserNotFoundException">Thrown when the driver is not found.</exception>
        /// <exception cref="Exception">Thrown when no vehicle is assigned to the driver or the driver has an ongoing or not started ride.</exception>

        public void ToggleAvailability(string driverEmail)
        {
            User? driver = GetDriver(driverEmail);
            if (driver == null)
            {
                throw new UserNotFoundException(ErrorMessages.UserNotDriver);
            }

            VehicleAndAvailability? vehicleAvailability = _context.VehiclesAndAvailability.FirstOrDefault(v => v.UserId == driver.UserId);
            if (vehicleAvailability == null)
            {
                throw new Exception(ErrorMessages.NoVehicleAssigned);
            }

            //to check if the driver is in a pending/ongoing ride
            bool hasOngoingOrNotStartedRide = _context.Rides.Any(r => r.DriverId == driver.UserId &&(r.Status==RideStatus.Ongoing|| r.Status==RideStatus.NotStarted));

            if (hasOngoingOrNotStartedRide) 
            {
                throw new OngoingOrPendingRideException(ErrorMessages.CannotToggeState);
            }

            vehicleAvailability.IsAvailable = !vehicleAvailability.IsAvailable;
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets the current ride of a driver.
        /// </summary>
        /// <param name="driverEmail">The email of the driver.</param>
        /// <returns>The current ride details.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the driver or rider is not found.</exception>
        /// <exception cref="OngoingOrPendingRideException">Thrown when there is no ongoing ride available.</exception>
        public RideResponseDriver GetCurrentRide(string driverEmail)
        {
            User? driver =GetDriver(driverEmail);
            if (driver == null)
            {
                throw new UserNotFoundException(ErrorMessages.DriverNotFound);
            }

            Ride? ongoingRide = _context.Rides.FirstOrDefault(r => r.DriverId == driver.UserId && r.Status == RideStatus.NotStarted);
            if (ongoingRide == null)
            {
                throw new OngoingOrPendingRideException(ErrorMessages.NoOngoingRideAvailable);
            }

            User? rider = _context.Users.FirstOrDefault(u => u.UserId == ongoingRide.RiderId);
            if (rider == null)
            {
                throw new UserNotFoundException(ErrorMessages.RiderNotFound);
            }

            return new RideResponseDriver
            {
                RiderName = rider.Name,
                RideStatus = ongoingRide.Status.ToString(),
                PickupLocation = ongoingRide.PickupLocation,
                DropLocation = ongoingRide.DropLocation
            };
        }
        /// <summary>
        /// Starts a ride for a driver.
        /// </summary>
        /// <param name="startRideRequest">The request containing ride start details.</param>
        /// <param name="driverEmail">The email of the driver.</param>
        /// <returns>The response containing ride start details.</returns>
        /// <exception cref="UserNotFoundException">Thrown when the driver is not found.</exception>
        /// <exception cref="RideNotFoundException">Thrown when the ride is not found or does not belong to the driver.</exception>
        /// <exception cref="InvalidOTPException">Thrown when the OTP is invalid.</exception>
        /// <exception cref="Exception">Thrown when the ride is not in "NotStarted" status.</exception>
        public StartRideResponse StartRide(StartRideRequest startRideRequest, string driverEmail)
        {
            User? driver = GetDriver(driverEmail);
            if (driver == null)
            {
                throw new UserNotFoundException(ErrorMessages.UserNotDriver);
            }

            Ride? ride = _context.Rides.FirstOrDefault(r => r.Id == startRideRequest.RideId);
            
            if (ride == null||ride.DriverId != driver.UserId)
            {
                throw new RideNotFoundException(ErrorMessages.RideNotFound);
            }

            if (ride.OTP != startRideRequest.OTP)
            {
                throw new InvalidOTPException(ErrorMessages.InvalidOTP);
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
        /// <summary>
        /// Rates a rider.
        /// </summary>
        /// <param name="rateRiderRequest">The request containing rating details.</param>
        /// <param name="driverEmail">The email of the driver.</param>
        /// <exception cref="RideNotFoundException">Thrown when the ride is not found or does not belong to the driver.</exception>
        /// <exception cref="Exception">Thrown when the ride is not completed or the rating is invalid.</exception>
        /// <exception cref="AlreadyRatedException">Thrown when the rider has already been rated.</exception>
        public void RateRider(RateRequest rateRiderRequest, string driverEmail)
        {
            Ride? ride = _context.Rides.FirstOrDefault(r => r.Id.ToString() == rateRiderRequest.RideId);

            if (ride == null)
            {
                throw new RideNotFoundException(ErrorMessages.RideNotFound);
            }

            User? driver = _context.Users.FirstOrDefault(u => u.Email == driverEmail);
            if (ride.DriverId != driver.UserId)
            {
                throw new RideNotFoundException(ErrorMessages.RideNotFound);
            }

            if (ride.Status != RideStatus.Completed)
            {
                throw new Exception(ErrorMessages.CannotRateRideNotEnded);
            }

            if (rateRiderRequest.Rating < 1 || rateRiderRequest.Rating > 5)
            {
                throw new ArgumentException(ErrorMessages.InvalidRating);
            }

            Ratings? existingRating = _context.Ratings.FirstOrDefault(r => r.RideId == ride.Id && r.RatedBy == UserType.Driver);
            if (existingRating != null)
            {
                throw new AlreadyRatedException(ErrorMessages.AlreadyRatedRider);
            }

            Ratings rating = RatingsMapper.MapToRatings(ride.Id, rateRiderRequest.Rating, UserType.Driver);

            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets a driver by email.
        /// </summary>
        /// <param name="driverEmail">The email of the driver.</param>
        /// <returns>The driver entity.</returns>
        private User? GetDriver(string driverEmail)
        {
            return _context.Users.FirstOrDefault(u => u.Email == driverEmail && u.UserType == UserType.Driver);
        }
    }
}

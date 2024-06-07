using Assessment_1.Constants;
using Assessment_1.DBContext;
using Assessment_1.Entitites;
using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Interfaces.IService;

namespace Assessment_1.Services
{
    public class RideService: IRideService
    {
        private readonly TaxiService _context;

        public RideService(TaxiService context)
        {
            _context = context;
        }

        /// <summary>
        /// Cancels a ride.
        /// </summary>
        /// <param name="rideId">The ID of the ride to cancel.</param>
        /// <param name="email">The email of the user requesting the cancellation.</param>
        /// <param name="userType">The type of the user (Rider or Driver).</param>
        /// <exception cref="RideNotFoundException">Thrown when the ride or user is not found.</exception>
        /// <exception cref="Exception">Thrown when the ride is not in a cancellable state.</exception>
        public void CancelRide(string rideId, string email, UserType userType)
        {
            Ride? ride = _context.Rides.FirstOrDefault(r => r.Id.ToString() == rideId);

            if (ride == null)
            {
                throw new Exception(ErrorMessages.RideNotFound);
            }

            User? user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user!=null && ((userType == UserType.Rider && ride.RiderId != user.UserId) ||
                (userType == UserType.Driver && ride.DriverId != user.UserId)))
            {
                throw new RideNotFoundException(ErrorMessages.RideNotFound);
            }

            if (ride.Status != RideStatus.NotStarted)
            {
                throw new Exception(ErrorMessages.CannotCancelRideStarted);
            }

            ride.Status = RideStatus.Cancelled;
            VehicleAndAvailability driver = _context.VehiclesAndAvailability.FirstOrDefault(u => u.UserId == ride.DriverId);
            driver.IsAvailable = true;
            _context.SaveChanges();
        }

        /// <summary>
        /// Ends a ride.
        /// </summary>
        /// <param name="rideId">The ID of the ride to end.</param>
        /// <param name="email">The email of the user requesting to end the ride.</param>
        /// <param name="userType">The type of the user (Rider or Driver).</param>
        /// <exception cref="RideNotFoundException">Thrown when the ride or user is not found.</exception>
        /// <exception cref="OngoingOrPendingRideException">Thrown when the ride is not in a state that can be ended.</exception>
        public void EndRide(string rideId, string email, UserType userType)
        {
            Ride? ride = _context.Rides.FirstOrDefault(r => r.Id.ToString() == rideId);

            if (ride == null)
            {
                throw new RideNotFoundException(ErrorMessages.RideNotFound);
            }

            User? user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null && ((userType == UserType.Rider && ride.RiderId != user.UserId) ||
                (userType == UserType.Driver && ride.DriverId != user.UserId)))
            {
                throw new RideNotFoundException(ErrorMessages.RideNotFound);
            }

            if (ride.Status != RideStatus.Ongoing)
            {
                throw new OngoingOrPendingRideException(ErrorMessages.CannotEndRideNotStarted);
            }

            ride.Status = RideStatus.Completed;
            ride.EndAt = DateTime.Now;
            VehicleAndAvailability driver = _context.VehiclesAndAvailability.FirstOrDefault(u => u.UserId == ride.DriverId);
            driver.IsAvailable = true;
            _context.SaveChanges();
        }
    }
}

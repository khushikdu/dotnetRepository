using Assessment_1.Constants;
using Assessment_1.DBContext;
using Assessment_1.Enums;
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
        public void CancelRide(string rideId, string email, UserType userType)
        {
            var ride = _context.Rides.FirstOrDefault(r => r.Id.ToString() == rideId);

            if (ride == null)
            {
                throw new Exception(ErrorMessages.RideNotFound);
            }

            if ((userType == UserType.Rider && ride.RiderId != _context.Users.FirstOrDefault(u => u.Email == email).UserId) ||
                (userType == UserType.Driver && ride.DriverId != _context.Users.FirstOrDefault(u => u.Email == email).UserId))
            {
                throw new Exception(ErrorMessages.RideNotFound);
            }

            if (ride.Status != RideStatus.NotStarted)
            {
                throw new Exception(ErrorMessages.CannotCancelRideStarted);
            }

            ride.Status = RideStatus.Cancelled;
            _context.SaveChanges();
        }

        public void EndRide(string rideId, string email, UserType userType)
        {
            var ride = _context.Rides.FirstOrDefault(r => r.Id.ToString() == rideId);

            if (ride == null)
            {
                throw new Exception(ErrorMessages.RideNotFound);
            }

            if ((userType == UserType.Rider && ride.RiderId != _context.Users.FirstOrDefault(u => u.Email == email).UserId) ||
                (userType == UserType.Driver && ride.DriverId != _context.Users.FirstOrDefault(u => u.Email == email).UserId))
            {
                throw new Exception(ErrorMessages.RideNotFound);
            }

            if (ride.Status != RideStatus.Ongoing)
            {
                throw new Exception(ErrorMessages.CannotEndRideNotStarted);
            }

            ride.Status = RideStatus.Completed;
            ride.EndAt = DateTime.Now;
            _context.SaveChanges();
        }
    }
}

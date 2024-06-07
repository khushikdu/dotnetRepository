using Assessment_1.Enums;

namespace Assessment_1.Interfaces.IService
{
    public interface IRideService
    {
        void CancelRide(string rideId, string email, UserType userType);
        void EndRide(string rideId, string email, UserType userType);
    }
}

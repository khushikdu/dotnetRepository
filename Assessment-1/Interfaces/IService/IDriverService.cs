using Assessment_1.Models.Request;
using Assessment_1.Models.Response;

namespace Assessment_1.Interfaces.IService
{
    public interface IDriverService
    {
        void ToggleAvailability(string driverEmail);
        RideResponseDriver GetCurrentRide(string driverEmail);
        StartRideResponse StartRide(StartRideRequest startRideRequest, string driverEmail);
        void RateRider(RateRequest rateRiderRequest, string driverEmail);
    }
}

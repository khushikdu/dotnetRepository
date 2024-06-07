using Assessment_1.Models.Request;
using Assessment_1.Models.Response;

namespace Assessment_1.Interfaces.IService
{
    public interface IRiderService
    {
        RideResponseRider RequestRide(string riderEmail, RideRequest rideRequest);
        RideResponseRider GetCurrentRide(string riderEmail);
        void RateDriver(RateRequest rateDriverRequest, string riderEmail);

    }
}

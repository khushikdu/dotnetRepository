using Assessment_1.ViewModel.RequestVM;

namespace Assessment_1.Interfaces.IService
{
    public interface IRiderService
    {
        int AddRider(AddRiderVM addRider);
        int BookRide(RequestRideVM requestRide, int riderId);

    }
}

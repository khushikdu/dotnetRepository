using Assessment_1.Entity;
using Assessment_1.ViewModel.RequestVM;

namespace Assessment_1.Interfaces.IRepository
{
    public interface IDriverRepository
    {
        int AddDriver(AddDriverVM driverVM);
        Driver GetDriverByRiderEmail(string email);
        List<Driver> GetAllAvailableDrivers();
        List<Driver> GetDriversByVehicleType(string vehicleType);

        void StartRide(int rideID,int driverId, int otp);
    }
}

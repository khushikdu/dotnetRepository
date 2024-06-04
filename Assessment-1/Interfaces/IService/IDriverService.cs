using Assessment_1.Entity;
using Assessment_1.ViewModel.RequestVM;

namespace Assessment_1.Interfaces.IService
{
    public interface IDriverService
    {
        int AddDriver(AddDriverVM driverVM);
        List<Driver> GetAllAvailableDrivers();
        List<Driver> GetDriversByVehicleType(string vehicleType);
    }
}

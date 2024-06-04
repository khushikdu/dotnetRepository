using Assessment_1.Entity;
using Assessment_1.ViewModel.RequestVM;

namespace Assessment_1.Interfaces.IRepository
{
    public interface IDriverRepository
    {
        int AddDriver(AddDriverVM driverVM);
        Driver GetRiderByRiderEmail(string email);
        List<Driver> GetAllAvailableDrivers();
        List<Driver> GetDriversByVehicleType(string vehicleType);
    }
}

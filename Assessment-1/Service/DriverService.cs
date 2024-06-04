using Assessment_1.Entity;
using Assessment_1.Interfaces.IRepository;
using Assessment_1.Interfaces.IService;
using Assessment_1.ViewModel.RequestVM;
using Microsoft.EntityFrameworkCore;

namespace Assessment_1.Service
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public int AddDriver(AddDriverVM driverVM)
        {
            return _driverRepository.AddDriver(driverVM);
        }
        public List<Driver> GetAllAvailableDrivers()
        {
            return _driverRepository.GetAllAvailableDrivers();
        }

        public List<Driver> GetDriversByVehicleType(string vehicleType)
        {
            return _driverRepository.GetDriversByVehicleType(vehicleType);
        }
    }
}

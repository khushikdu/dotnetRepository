using Assessment_1.DBContext;
using Assessment_1.Entity;
using Assessment_1.Enums;
using Assessment_1.Interfaces.IRepository;
using Assessment_1.ViewModel.RequestVM;

namespace Assessment_1.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly TaxiServiceDbContext _context;

        public DriverRepository(TaxiServiceDbContext context)
        {
            _context = context;
        }

        public int AddDriver(AddDriverVM driverVM)
        {
            var existingCustomer = _context.Riders.SingleOrDefault(c => c.Email == driverVM.Email);

            if (existingCustomer != null)
            {
                throw new InvalidOperationException("Driver with the same email already exists.");
            }
            var driver = new Driver
            {
                Name = driverVM.Name,
                Email = driverVM.Email,
                Password = driverVM.Password,
                Phone = driverVM.Phone,
                VehicleType = Enum.Parse<VehicleType>(driverVM.VehicleType).ToString(),
                VehicleNumber = driverVM.VehicleNumber,
                Status = Availability.Available.ToString(),
            };

            _context.Drivers.Add(driver);
            _context.SaveChanges();
            return driver.DriverId;
        }

        public Driver GetDriverByRiderEmail(string email)
        {
            return _context.Drivers.SingleOrDefault(r => r.Email == email);
        }

        public List<Driver> GetAllAvailableDrivers()
        {
            return _context.Drivers.Where(d => d.Status == "Available").ToList();
        }
        public List<Driver> GetDriversByVehicleType(string vehicleType)
        {
            return _context.Drivers.Where(d => d.VehicleType == vehicleType).ToList();
        }
        public void StartRide(int rideID, int driverId, int otp)
        {

            try
            {
                var driver = _context.Drivers.SingleOrDefault(d => d.DriverId == driverId);
                if (driver != null) 
                {
                    driver.Status= Availability.Unavailable.ToString();
                    _context.SaveChanges();
                }

                var ride = _context.Rides.SingleOrDefault(d => d.RiderId == rideID);
                if (driver != null)
                {
                    ride.Status = RideStatus.Ongoing.ToString();
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
    }
}


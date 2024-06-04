using Assessment_1.DBContext;
using Assessment_1.Entity;
using Assessment_1.Enums;
using Assessment_1.Interfaces.IRepository;
using Assessment_1.ViewModel.RequestVM;
using Assessment_1.ViewModel.ResponseVM;

namespace Assessment_1.Repository
{
    public class RiderRepository : IRiderRepository
    {
        private readonly TaxiServiceDbContext _context;
        private readonly IDriverRepository _driverRepository;

        public RiderRepository(TaxiServiceDbContext context, IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
            _context = context;
            
        }

        public int AddRider(AddRiderVM riderVM)
        {
            var existingCustomer = _context.Riders.SingleOrDefault(c => c.Email == riderVM.Email);

            if (existingCustomer != null)
            {
                throw new InvalidOperationException("Rider with the same email already exists.");
            }

            Rider newRider = new Rider
            {
                Name = riderVM.Name,
                Email = riderVM.Email,
                Password = riderVM.Password,
                Phone = riderVM.Phone,
            };

            _context.Riders.Add(newRider);
            _context.SaveChanges();

            return newRider.RiderId;
        }

        public Rider GetRiderByRiderEmail(string email)
        {
            return _context.Riders.SingleOrDefault(r => r.Email == email);
        }
        public int BookRide(Ride ride)
        {
            _context.Rides.Add(ride);
            _context.SaveChanges();
            return ride.RideId;
        }

    }
}

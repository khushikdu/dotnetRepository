using Assignment_3.DTO.RequestDTO;
using Assignment_3.Model;
using Assignment_3.Respository.IRepository;

namespace Assignment_3.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MySQLDBContext _context;

        public CustomerRepository(MySQLDBContext context)
        {
            _context = context;
        }

        public int AddCustomer(AddCustomerDTO customerDto)
        {
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.Email == customerDto.Email);

            if (existingCustomer != null)
            {
                throw new InvalidOperationException("Customer with the same email already exists.");
            }

            var newCustomer = new Customer
            {
                Username = customerDto.Username,
                Email = customerDto.Email
            };

            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            return newCustomer.Id;
        }
    }
}

using Assignment_3.DTO.RequestDTO;
using Assignment_3.Model;
using Assignment_3.Respository.IRepository;
using System;
using System.Linq;

namespace Assignment_3.Repositories
{
    /// <summary>
    /// Repository class for managing customer data operations.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MySQLDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CustomerRepository(MySQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customerDto">The data transfer object containing the customer information.</param>
        /// <returns>The ID of the newly added customer.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a customer with the same email already exists.</exception>
        public int AddCustomer(AddCustomerDTO customerDto)
        {
            // Check if a customer with the same email already exists.
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.Email == customerDto.Email);

            if (existingCustomer != null)
            {
                throw new InvalidOperationException("Customer with the same email already exists.");
            }

            // Create a new customer entity.
            var newCustomer = new Customer
            {
                Username = customerDto.Username,
                Email = customerDto.Email
            };

            // Add the new customer to the database context and save changes.
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            // Return the ID of the newly added customer.
            return newCustomer.Id;
        }
    }
}

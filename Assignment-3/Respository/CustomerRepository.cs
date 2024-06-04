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
            Customer? existingCustomer = _context.Customers.FirstOrDefault(c => c.Email == customerDto.Email);

            if (existingCustomer != null)
            {
                throw new InvalidOperationException("Customer with the same email already exists.");
            }

            Customer newCustomer = new Customer
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

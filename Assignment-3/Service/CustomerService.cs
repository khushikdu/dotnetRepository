using Assignment_3.DTO.RequestDTO;
using Assignment_3.Respository.IRepository;

namespace Assignment_3.Services
{
    /// <summary>
    /// Service class for managing customer-related operations.
    /// </summary>
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="customerRepository">The customer repository.</param>
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customerDto">The data transfer object containing the customer information.</param>
        /// <returns>The ID of the newly added customer.</returns>
        public int AddCustomer(AddCustomerDTO customerDto)
        {
            return _customerRepository.AddCustomer(customerDto);
        }
    }
}

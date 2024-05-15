using Assignment_3.DTO.RequestDTO;
using Assignment_3.Model;
using Assignment_3.Respository.IRepository;

namespace Assignment_3.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public int AddCustomer(AddCustomerDTO customerDto)
        {
            return _customerRepository.AddCustomer(customerDto);
        }
    }
}

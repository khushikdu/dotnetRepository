using Assignment_3.DTO.RequestDTO;
using Assignment_3.Respository.IRepository;

namespace Assignment_3.Services
{
    /// <summary>
    /// Service class for managing customer-related operations.
    /// </summary>
    public interface ICustomerService
    {
        int AddCustomer(AddCustomerDTO customerDto);
    }
}

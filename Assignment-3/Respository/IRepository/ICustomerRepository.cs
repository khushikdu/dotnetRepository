using Assignment_3.DTO.RequestDTO;

namespace Assignment_3.Respository.IRepository
{
    /// <summary>
    /// Interface for managing customer data operations.
    /// </summary>
    public interface ICustomerRepository
    {
        int AddCustomer(AddCustomerDTO customerDto);
    }
}

using Assignment_3.DTO.RequestDTO;

namespace Assignment_3.Respository.IRepository
{
    /// <summary>
    /// Interface for managing customer data operations.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customerDto">The data transfer object containing the customer information.</param>
        /// <returns>The ID of the newly added customer.</returns>
        int AddCustomer(AddCustomerDTO customerDto);
    }
}

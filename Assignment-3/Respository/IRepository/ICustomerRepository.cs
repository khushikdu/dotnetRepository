using Assignment_3.DTO.RequestDTO;

namespace Assignment_3.Respository.IRepository
{
    public interface ICustomerRepository
    {
        int AddCustomer(AddCustomerDTO customerDto);
    }
}

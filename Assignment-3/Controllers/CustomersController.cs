using Microsoft.AspNetCore.Mvc;
using Assignment_3.DTO.RequestDTO;
using Assignment_3.Services;

namespace Assignment_3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public ActionResult<int> AddCustomer(AddCustomerDTO customerDto)
        {
            try
            {
                int customerId = _customerService.AddCustomer(customerDto);
                return Ok(customerId);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}

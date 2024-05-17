using Microsoft.AspNetCore.Mvc;
using Assignment_3.DTO.RequestDTO;
using Assignment_3.Services;

namespace Assignment_3.Controllers
{
    /// <summary>
    /// Controller for managing customer-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController"/> class.
        /// </summary>
        /// <param name="customerService">The service used to manage customers.</param>
        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customerDto">The data transfer object containing the customer information.</param>
        /// <returns>The ID of the newly added customer if successful, or a conflict status if an error occurs.</returns>
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

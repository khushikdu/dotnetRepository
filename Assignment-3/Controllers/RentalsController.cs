using Microsoft.AspNetCore.Mvc;
using Assignment_3.DTO.RequestDTO;
using Assignment_3.Services;

namespace Assignment_3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly RentalService _rentalService;

        public RentalsController(RentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost("byId")]
        public IActionResult RentMovieById(RentMovieByID_DTO rentDto)
        {
            try
            {
                _rentalService.RentMovieById(rentDto);
                return Ok("Movie rented successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("byName")]
        public IActionResult RentMovieByTitle(RentMovieByName_DTO rentDto)
        {
            try
            {
                _rentalService.RentMovieByTitle(rentDto);
                return Ok("Movie rented successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("customers/{movieId}")]
        public IActionResult GetCustomersByMovieId(int movieId)
        {
            var customers = _rentalService.GetCustomersByMovieId(movieId);
            return Ok(customers);
        }

        [HttpGet("movies/{customerId}")]
        public IActionResult GetMoviesByCustomerId(int customerId)
        {
            var movies = _rentalService.GetMoviesByCustomerId(customerId);
            return Ok(movies);
        }

        [HttpGet("totalCost/{customerId}")]
        public IActionResult GetTotalCostByCustomerId(int customerId)
        {
            int totalCost = _rentalService.GetTotalCostByCustomerId(customerId);
            return Ok(totalCost);
        }
    }
}

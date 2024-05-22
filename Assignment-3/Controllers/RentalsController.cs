using Microsoft.AspNetCore.Mvc;
using Assignment_3.DTO.RequestDTO;
using Assignment_3.Services;

namespace Assignment_3.Controllers
{
    /// <summary>
    /// Controller for managing rental-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalsController"/> class.
        /// </summary>
        /// <param name="rentalService">The service used to manage rentals.</param>
        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        /// <summary>
        /// Rents a movie by its ID.
        /// </summary>
        /// <param name="rentDto">The data transfer object containing the rental information.</param>
        /// <returns>A success message if the movie is rented successfully, or a conflict status if an error occurs.</returns>
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

        /// <summary>
        /// Rents a movie by its title.
        /// </summary>
        /// <param name="rentDto">The data transfer object containing the rental information.</param>
        /// <returns>A success message if the movie is rented successfully, or a conflict status if an error occurs.</returns>
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

        /// <summary>
        /// Gets the customers who rented a specific movie by its ID.
        /// </summary>
        /// <param name="movieId">The ID of the movie.</param>
        /// <returns>A list of customers who rented the specified movie.</returns>
        [HttpGet("customers/{movieId}")]
        public IActionResult GetCustomersByMovieId(int movieId)
        {
            var customers = _rentalService.GetCustomersByMovieId(movieId);
            return Ok(customers);
        }

        /// <summary>
        /// Gets the movies rented by a specific customer by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>A list of movies rented by the specified customer.</returns>
        [HttpGet("movies/{customerId}")]
        public IActionResult GetMoviesByCustomerId(int customerId)
        {
            var movies = _rentalService.GetMoviesByCustomerId(customerId);
            return Ok(movies);
        }

        /// <summary>
        /// Gets the total rental cost for a specific customer by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>The total rental cost for the specified customer.</returns>
        [HttpGet("totalCost/{customerId}")]
        public IActionResult GetTotalCostByCustomerId(int customerId)
        {
            int totalCost = _rentalService.GetTotalCostByCustomerId(customerId);
            return Ok(totalCost);
        }
    }
}

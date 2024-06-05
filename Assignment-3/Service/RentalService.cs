using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Respository.IRepository;
using System.Collections.Generic;

namespace Assignment_3.Services
{
    /// <summary>
    /// Service class for managing rental-related operations.
    /// </summary>
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        /// <summary>
        /// Rents a movie by its ID.
        /// </summary>
        /// <param name="rentDto">The data transfer object containing the rental information.</param>
        public void RentMovieById(RentMovieByID_DTO rentDto)
        {
            _rentalRepository.RentMovieById(rentDto);
        }

        /// <summary>
        /// Rents a movie by its title.
        /// </summary>
        /// <param name="rentDto">The data transfer object containing the rental information.</param>
        public void RentMovieByTitle(RentMovieByName_DTO rentDto)
        {
            _rentalRepository.RentMovieByTitle(rentDto);
        }

        /// <summary>
        /// Gets the customers who rented a specific movie by its ID.
        /// </summary>
        /// <param name="movieId">The ID of the movie.</param>
        /// <returns>A collection of customers who rented the specified movie.</returns>
        public IEnumerable<CustomerResponseDTO> GetCustomersByMovieId(int movieId)
        {
            return _rentalRepository.GetCustomersByMovieId(movieId);
        }

        /// <summary>
        /// Gets the movies rented by a specific customer by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>A collection of movies rented by the specified customer.</returns>
        public IEnumerable<MovieResponseDTO> GetMoviesByCustomerId(int customerId)
        {
            return _rentalRepository.GetMoviesByCustomerId(customerId);
        }

        /// <summary>
        /// Gets the total rental cost for a specific customer by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>The total rental cost for the specified customer.</returns>
        public int GetTotalCostByCustomerId(int customerId)
        {
            return _rentalRepository.GetTotalCostByCustomerId(customerId);
        }
    }
}

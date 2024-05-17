using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using System.Collections.Generic;

namespace Assignment_3.Respository.IRepository
{
    /// <summary>
    /// Interface for managing rental data operations.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Rents a movie by its ID.
        /// </summary>
        /// <param name="rentDto">The data transfer object containing the rental information.</param>
        void RentMovieById(RentMovieByID_DTO rentDto);

        /// <summary>
        /// Rents a movie by its title.
        /// </summary>
        /// <param name="rentDto">The data transfer object containing the rental information.</param>
        void RentMovieByTitle(RentMovieByName_DTO rentDto);

        /// <summary>
        /// Gets the customers who rented a specific movie by its ID.
        /// </summary>
        /// <param name="movieId">The ID of the movie.</param>
        /// <returns>A collection of customers who rented the specified movie.</returns>
        IEnumerable<CustomerResponseDTO> GetCustomersByMovieId(int movieId);

        /// <summary>
        /// Gets the movies rented by a specific customer by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>A collection of movies rented by the specified customer.</returns>
        IEnumerable<MovieResponseDTO> GetMoviesByCustomerId(int customerId);

        /// <summary>
        /// Gets the total rental cost for a specific customer by their ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>The total rental cost for the specified customer.</returns>
        int GetTotalCostByCustomerId(int customerId);
    }
}

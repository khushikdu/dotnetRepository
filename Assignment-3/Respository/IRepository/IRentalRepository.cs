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
        void RentMovieById(RentMovieByID_DTO rentDto);
        void RentMovieByTitle(RentMovieByName_DTO rentDto);
        IEnumerable<CustomerResponseDTO> GetCustomersByMovieId(int movieId);
        IEnumerable<MovieResponseDTO> GetMoviesByCustomerId(int customerId);
        int GetTotalCostByCustomerId(int customerId);
    }
}

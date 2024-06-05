using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Respository.IRepository;
using System.Collections.Generic;

namespace Assignment_3.Services
{
    /// <summary>
    /// Service class for managing rental-related operations.
    /// </summary>
    public interface IRentalService
    {
        void RentMovieById(RentMovieByID_DTO rentDto);
        void RentMovieByTitle(RentMovieByName_DTO rentDto);
        IEnumerable<CustomerResponseDTO> GetCustomersByMovieId(int movieId);
        IEnumerable<MovieResponseDTO> GetMoviesByCustomerId(int customerId);
        int GetTotalCostByCustomerId(int customerId);
    }
}

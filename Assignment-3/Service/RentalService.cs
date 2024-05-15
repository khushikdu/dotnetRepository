using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Model;
using Assignment_3.Respository.IRepository;

namespace Assignment_3.Services
{
    public class RentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public void RentMovieById(RentMovieByID_DTO rentDto)
        {
            _rentalRepository.RentMovieById(rentDto);
        }

        public void RentMovieByTitle(RentMovieByName_DTO rentDto)
        {
            _rentalRepository.RentMovieByTitle(rentDto);
        }

        public IEnumerable<CustomerResponseDTO> GetCustomersByMovieId(int movieId)
        {
            return _rentalRepository.GetCustomersByMovieId(movieId);
        }

        public IEnumerable<MovieResponseDTO> GetMoviesByCustomerId(int customerId)
        {
            return _rentalRepository.GetMoviesByCustomerId(customerId);
        }
        public int GetTotalCostByCustomerId(int customerId)
        {
            return _rentalRepository.GetTotalCostByCustomerId(customerId);
        }
    }
}

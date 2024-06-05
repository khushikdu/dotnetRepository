using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Respository.IRepository;
using System.Collections.Generic;

namespace Assignment_3.Service
{
    /// <summary>
    /// Service interface for managing movie-related operations.
    /// </summary>
    public interface IMovieService
    {
        int AddMovie(AddMovieDTO movieDto);
        IEnumerable<MovieResponseDTO> GetAllMovies();
        MovieResponseDTO GetMovieById(int id);
        MovieResponseDTO GetMovieByTitle(string title);
    }
}

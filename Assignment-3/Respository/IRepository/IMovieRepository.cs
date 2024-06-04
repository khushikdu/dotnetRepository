using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using System.Collections.Generic;

namespace Assignment_3.Respository.IRepository
{
    /// <summary>
    /// Interface for managing movie data operations.
    /// </summary>
    public interface IMovieRepository
    {

        int AddMovie(AddMovieDTO movieDto);

        IEnumerable<MovieResponseDTO> GetAllMovies();

        MovieResponseDTO GetMovieById(int id);

        MovieResponseDTO GetMovieByTitle(string title);
    }
}

using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;

namespace Assignment_3.Respository.IRepository
{
    public interface IMovieRepository
    {
        int AddMovie(AddMovieDTO movieDto);
        IEnumerable<MovieResponseDTO> GetAllMovies();
        MovieResponseDTO GetMovieById(int id);
        MovieResponseDTO GetMovieByTitle(string title);
    }
}

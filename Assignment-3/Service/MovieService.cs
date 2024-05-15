using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Respository.IRepository;

namespace Assignment_3.Service
{
    public class MovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public int AddMovie(AddMovieDTO movieDto)
        {
            return _movieRepository.AddMovie(movieDto);
        }
        public IEnumerable<MovieResponseDTO> GetAllMovies()
        {
            return _movieRepository.GetAllMovies();
        }

        public MovieResponseDTO GetMovieById(int id)
        {
            return _movieRepository.GetMovieById(id);
        }

        public MovieResponseDTO GetMovieByTitle(string title)
        {
            return _movieRepository.GetMovieByTitle(title);
        }
    }
}

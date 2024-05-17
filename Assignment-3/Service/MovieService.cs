using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Respository.IRepository;
using System.Collections.Generic;

namespace Assignment_3.Service
{
    /// <summary>
    /// Service class for managing movie-related operations.
    /// </summary>
    public class MovieService
    {
        private readonly IMovieRepository _movieRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieService"/> class.
        /// </summary>
        /// <param name="movieRepository">The movie repository.</param>
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Adds a new movie.
        /// </summary>
        /// <param name="movieDto">The data transfer object containing the movie information.</param>
        /// <returns>The ID of the newly added movie.</returns>
        public int AddMovie(AddMovieDTO movieDto)
        {
            return _movieRepository.AddMovie(movieDto);
        }

        /// <summary>
        /// Retrieves all movies.
        /// </summary>
        /// <returns>A collection of all movies.</returns>
        public IEnumerable<MovieResponseDTO> GetAllMovies()
        {
            return _movieRepository.GetAllMovies();
        }

        /// <summary>
        /// Retrieves a movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie with the specified ID.</returns>
        public MovieResponseDTO GetMovieById(int id)
        {
            return _movieRepository.GetMovieById(id);
        }

        /// <summary>
        /// Retrieves a movie by its title.
        /// </summary>
        /// <param name="title">The title of the movie.</param>
        /// <returns>The movie with the specified title.</returns>
        public MovieResponseDTO GetMovieByTitle(string title)
        {
            return _movieRepository.GetMovieByTitle(title);
        }
    }
}

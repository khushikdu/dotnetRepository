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
        /// <summary>
        /// Adds a new movie.
        /// </summary>
        /// <param name="movieDto">The data transfer object containing the movie information.</param>
        /// <returns>The ID of the newly added movie.</returns>
        int AddMovie(AddMovieDTO movieDto);

        /// <summary>
        /// Gets all movies.
        /// </summary>
        /// <returns>A collection of all movies.</returns>
        IEnumerable<MovieResponseDTO> GetAllMovies();

        /// <summary>
        /// Gets a movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie with the specified ID.</returns>
        MovieResponseDTO GetMovieById(int id);

        /// <summary>
        /// Gets a movie by its title.
        /// </summary>
        /// <param name="title">The title of the movie.</param>
        /// <returns>The movie with the specified title.</returns>
        MovieResponseDTO GetMovieByTitle(string title);
    }
}

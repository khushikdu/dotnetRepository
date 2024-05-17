using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Model;
using Assignment_3.Respository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_3.Respository
{
    /// <summary>
    /// Repository class for managing movie data operations.
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        private readonly MySQLDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public MovieRepository(MySQLDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new movie.
        /// </summary>
        /// <param name="movieDto">The data transfer object containing the movie information.</param>
        /// <returns>The ID of the newly added movie.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a movie with the same title already exists.</exception>
        public int AddMovie(AddMovieDTO movieDto)
        {
            // Check if a movie with the same title already exists.
            var existingMovie = _context.Movies.FirstOrDefault(m => m.Title == movieDto.Title);

            if (existingMovie != null)
            {
                throw new InvalidOperationException("Movie with the same title already exists.");
            }

            // Create a new movie entity.
            var newMovie = new Movie
            {
                Title = movieDto.Title,
                Price = movieDto.Price
            };

            // Add the new movie to the database context and save changes.
            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            // Return the ID of the newly added movie.
            return newMovie.Id;
        }

        /// <summary>
        /// Retrieves all movies.
        /// </summary>
        /// <returns>A collection of all movies.</returns>
        public IEnumerable<MovieResponseDTO> GetAllMovies()
        {
            return _context.Movies
                .Select(movie => new MovieResponseDTO
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Price = movie.Price
                })
                .ToList();
        }

        /// <summary>
        /// Retrieves a movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie with the specified ID, or null if not found.</returns>
        public MovieResponseDTO GetMovieById(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return null;
            }

            return new MovieResponseDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Price = movie.Price
            };
        }

        /// <summary>
        /// Retrieves a movie by its title.
        /// </summary>
        /// <param name="title">The title of the movie.</param>
        /// <returns>The movie with the specified title, or null if not found.</returns>
        public MovieResponseDTO GetMovieByTitle(string title)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Title == title);
            if (movie == null)
            {
                return null;
            }

            return new MovieResponseDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Price = movie.Price
            };
        }
    }
}

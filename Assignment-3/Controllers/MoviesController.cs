using Microsoft.AspNetCore.Mvc;
using Assignment_3.DTO.RequestDTO;
using Assignment_3.Service;
using Assignment_3.Model;
using Assignment_3.DTO.ResponseDTO;
using System.Collections.Generic;

namespace Assignment_3.Controllers
{
    /// <summary>
    /// Controller for managing movie-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MySQLDBContext _dbContext;
        private readonly IMovieService _movieService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesController"/> class.
        /// </summary>
        /// <param name="movieService">The service used to manage movies.</param>
        /// <param name="context">The database context.</param>
        public MoviesController(IMovieService movieService, MySQLDBContext context)
        {
            _dbContext = context;
            _movieService = movieService;
        }

        /// <summary>
        /// Adds a new movie.
        /// </summary>
        /// <param name="movieDto">The data transfer object containing the movie information.</param>
        /// <returns>The ID of the newly added movie if successful, or a conflict status if an error occurs.</returns>
        [HttpPost]
        public ActionResult<int> AddMovie(AddMovieDTO movieDto)
        {
            try
            {
                int movieId = _movieService.AddMovie(movieDto);
                return Ok(movieId);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Gets all movies.
        /// </summary>
        /// <returns>A list of movies.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<MovieResponseDTO>> GetAllMovies()
        {
            var movies = _movieService.GetAllMovies();
            return Ok(movies);
        }

        /// <summary>
        /// Gets a movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie.</param>
        /// <returns>The movie with the specified ID, or a not found status if the movie does not exist.</returns>
        [HttpGet("{id}")]
        public ActionResult<MovieResponseDTO> GetMovieById(int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }
            return Ok(movie);
        }

        /// <summary>
        /// Gets a movie by its title.
        /// </summary>
        /// <param name="title">The title of the movie.</param>
        /// <returns>The movie with the specified title, or a not found status if the movie does not exist.</returns>
        [HttpGet("title/{title}")]
        public ActionResult<MovieResponseDTO> GetMovieByTitle(string title)
        {
            var movie = _movieService.GetMovieByTitle(title);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }
            return Ok(movie);
        }
    }
}

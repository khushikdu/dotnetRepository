using Microsoft.AspNetCore.Mvc;
using Assignment_3.DTO.RequestDTO;
using Assignment_3.Service;
using Assignment_3.Model;
using Assignment_3.DTO.ResponseDTO;

namespace Assignment_3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private MySQLDBContext _dbContext;
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService, MySQLDBContext context)
        {
            _dbContext = context;
            _movieService = movieService;
        }

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
        [HttpGet]
        public ActionResult<IEnumerable<MovieResponseDTO>> GetAllMovies()
        {
            var movies = _movieService.GetAllMovies();
            return Ok(movies);
        }

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

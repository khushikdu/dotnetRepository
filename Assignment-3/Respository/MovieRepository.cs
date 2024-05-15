using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Model;
using Assignment_3.Respository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3.Respository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MySQLDBContext _context;

        public MovieRepository(MySQLDBContext context)
        {
            _context = context;
        }

        public int AddMovie(AddMovieDTO movieDto)
        {
            var existingMovie = _context.Movies.FirstOrDefault(m => m.Title == movieDto.Title);

            if (existingMovie != null)
            {
                // Movie with the same title already exists
                throw new InvalidOperationException("Movie with the same title already exists.");
            }

            var newMovie = new Movie
            {
                Title = movieDto.Title,
                Price = movieDto.Price
            };

            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            return newMovie.Id;
        }
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

        public MovieResponseDTO GetMovieById(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return null;
            }
            
            foreach(var customers in movie.Rentals)
            {
                Console.WriteLine(customers.Customer.Username);
            }

            return new MovieResponseDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Price = movie.Price
            };
        }

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


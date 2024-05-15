using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Model;
using Assignment_3.Respository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Assignment_3.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly MySQLDBContext _dbContext;
        private readonly IMovieRepository _movieRepository;
        private readonly ICustomerRepository _customerRepository;

        public RentalRepository(MySQLDBContext context, IMovieRepository movieRepository, ICustomerRepository customerRepository)
        {
            _dbContext = context;
            _movieRepository = movieRepository;
            _customerRepository = customerRepository;
        }

        public void RentMovieById(RentMovieByID_DTO rentDto)
        {
            try
            {
                var movie = _dbContext.Movies
                                    .Include(m => m.Rentals)
                                    .SingleOrDefault(m => m.Id == rentDto.MovieId);
                //var movie = _movieRepository.GetMovieById(rentDto.MovieId);
                var customer = _dbContext.Customers.Find(rentDto.CustomerId);

                if (movie == null || customer == null)
                {
                    throw new InvalidOperationException("Invalid movie ID or customer ID.");
                }

                Console.WriteLine($"Movie: {movie.Title} ({movie.Id})");
                Console.WriteLine($"Customer: {customer.Username} ({customer.Id})");

                var rental = new Rental
                {
                    MovieId = movie.Id,
                    Movie = movie,
                    CustomerId = customer.Id,
                    Customer = customer,
                    RentalDate = DateTime.Now
                };
                
                movie.Rentals.Add(rental);
                Console.WriteLine($"Movie Count : {movie.Rentals.Count}");

               
                customer.Rentals.Add(rental);

                Console.WriteLine($"Movie Count : {movie.Rentals.Count}");
                Console.WriteLine($"Customer Count : {customer.Rentals.Count}");

                Console.WriteLine($"Rental Date: {rental.RentalDate}");
                Console.WriteLine($"Rental Movie: {rental.Movie.Title} ({rental.Movie.Id})");
                Console.WriteLine($"Rental Customer: {rental.Customer.Username} ({rental.Customer.Id})");

                _dbContext.Rentals.Add(rental);
                _dbContext.SaveChanges();

                Console.WriteLine("Rental successfully added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while renting movie: {ex.Message}");
                throw;
            }
        }

        public void RentMovieByTitle(RentMovieByName_DTO rentDto)
        {
            try
            {

                var movie = _dbContext.Movies.FirstOrDefault(m => m.Title == rentDto.Title);
                var customer = _dbContext.Customers.FirstOrDefault(c => c.Username == rentDto.Username);

                if (movie == null || customer == null)
                {
                    throw new InvalidOperationException("Invalid movie ID or customer ID.");
                }

                Console.WriteLine($"Movie: {movie.Title} ({movie.Id})");
                Console.WriteLine($"Customer: {customer.Username} ({customer.Id})");

                var rental = new Rental
                {
                    MovieId = movie.Id,
                    Movie = movie,
                    CustomerId = customer.Id,
                    Customer = customer,
                    RentalDate = DateTime.Now
                };

                Console.WriteLine($"Rental Date: {rental.RentalDate}");
                Console.WriteLine($"Rental Movie: {rental.Movie.Title} ({rental.Movie.Id})");
                Console.WriteLine($"Rental Customer: {rental.Customer.Username} ({rental.Customer.Id})");

                _dbContext.Rentals.Add(rental);
                _dbContext.SaveChanges();

                Console.WriteLine("Rental successfully added.");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while renting movie: {ex.Message}");
                throw;
            }
        }          

        public IEnumerable<CustomerResponseDTO> GetCustomersByMovieId(int movieId)
        {
            return _dbContext.Rentals
                .Include(r => r.Customer)
                .Where(r => r.MovieId == movieId)
                .Select(r => new CustomerResponseDTO
                {
                    Id = r.Customer.Id,
                    Username = r.Customer.Username,
                    Email = r.Customer.Email
                })
                .Distinct()
                .ToList();
            
        }

        public IEnumerable<MovieResponseDTO> GetMoviesByCustomerId(int customerId)
        {
            return _dbContext.Rentals
                .Include(r => r.Movie)
                .Where(r => r.CustomerId == customerId)
                .Select(r => new MovieResponseDTO
                {
                    Id = r.Movie.Id,
                    Title = r.Movie.Title,
                    Price = r.Movie.Price
                })
                .Distinct()
                .ToList();
        }
        public int GetTotalCostByCustomerId(int customerId)
        {
            var rentals = _dbContext.Rentals
                .Include(r => r.Movie) 
                .Where(r => r.CustomerId == customerId)
                .ToList();

            Console.WriteLine($"Total rentals count: {rentals.Count}");

            int totalCost = 0;

            foreach (var rental in rentals)
            {
                Console.WriteLine($"Rental Price: {rental.Movie?.Price}"); 
                if (rental.Movie != null)
                {
                    totalCost += rental.Movie.Price;
                }
            }

            return totalCost;
        }

    }
}

using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Model;
using Assignment_3.Respository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_3.Repositories
{
    /// <summary>
    /// Repository class for managing rental data operations.
    /// </summary>
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
                Movie? movie = _dbContext.Movies
                                    .Include(m => m.Rentals)
                                    .SingleOrDefault(m => m.Id == rentDto.MovieId);
                Customer? customer = _dbContext.Customers.Find(rentDto.CustomerId);

                if (movie == null || customer == null)
                {
                    throw new InvalidOperationException("Invalid movie ID or customer ID.");
                }

                // Check for existing rental
                Rental? existingRental = _dbContext.Rentals
                                              .Where(r => r.MovieId == movie.Id && r.CustomerId == customer.Id)
                                              .OrderByDescending(r => r.RentalDate)
                                              .FirstOrDefault();

                if (existingRental != null && existingRental.ReturnDate > DateTime.Now)
                {
                    throw new InvalidOperationException("Movie is already rented.");
                }

                Rental rental = new Rental
                {
                    MovieId = movie.Id,
                    CustomerId = customer.Id,
                    RentalDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(7)
                };

                movie.Rentals.Add(rental);
                customer.Rentals.Add(rental);

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

        /// <summary>
        /// Rents a movie by its title.
        /// </summary>
        /// <param name="rentDto">The data transfer object containing the rental information.</param>
        /// <exception cref="InvalidOperationException">Thrown when the movie title or customer username is invalid or the movie is already rented.</exception>
        public void RentMovieByTitle(RentMovieByName_DTO rentDto)
        {
            try
            {
                Movie? movie = _dbContext.Movies.FirstOrDefault(m => m.Title == rentDto.Title);
                Customer? customer = _dbContext.Customers.FirstOrDefault(c => c.Username == rentDto.Username);

                if (movie == null || customer == null)
                {
                    throw new InvalidOperationException("Invalid movie title or customer username.");
                }

                Rental? existingRental = _dbContext.Rentals
                                              .Where(r => r.MovieId == movie.Id && r.CustomerId == customer.Id)
                                              .OrderByDescending(r => r.RentalDate)
                                              .FirstOrDefault();

                if (existingRental != null && existingRental.ReturnDate > DateTime.Now)
                {
                    throw new InvalidOperationException("Movie is already rented.");
                }

                Rental rental = new Rental
                {
                    MovieId = movie.Id,
                    CustomerId = customer.Id,
                    RentalDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(7)
                };

                movie.Rentals.Add(rental);
                customer.Rentals.Add(rental);

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

        /// <summary>
        /// Retrieves customers who rented a specific movie by movie ID.
        /// </summary>
        /// <param name="movieId">The ID of the movie.</param>
        /// <returns>A collection of customers who rented the movie.</returns>
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

        /// <summary>
        /// Retrieves movies rented by a specific customer by customer ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>A collection of movies rented by the customer.</returns>
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

        /// <summary>
        /// Retrieves the total cost of movies rented by a specific customer by customer ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>The total cost of movies rented by the customer.</returns>
        public int GetTotalCostByCustomerId(int customerId)
        {
            List<Rental> rentals = _dbContext.Rentals
                .Include(r => r.Movie)
                .Where(r => r.CustomerId == customerId)
                .ToList();

            Console.WriteLine($"Total rentals count: {rentals.Count}");

            int totalCost = 0;

            foreach (Rental rental in rentals)
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

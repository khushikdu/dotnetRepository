using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Respository.IRepository;
using Assignment_3.Services;

namespace Assignment_3.Tests
{
    /// <summary>
    /// Unit tests for the RentalService class.
    /// </summary>
    public class RentalServiceTests
    {
        private readonly Mock<IRentalRepository> _mockRentalRepository;
        private readonly IRentalService _rentalService;

        /// <summary>
        /// Unit tests for the RentalService class.
        /// </summary>
        public RentalServiceTests()
        {
            _mockRentalRepository = new Mock<IRentalRepository>();
            _rentalService = new RentalService(_mockRentalRepository.Object);
        }

        /// <summary>
        /// Positive test for renting a movie by ID successfully.
        /// </summary>        
        [Fact]
        public void RentMovieById_ValidDto_Success()
        {
            // Arrange
            var rentDto = new RentMovieByID_DTO { MovieId = 1, CustomerId = 1 };
            _mockRentalRepository.Setup(repo => repo.RentMovieById(rentDto)).Verifiable();

            // Act
            _rentalService.RentMovieById(rentDto);

            // Assert
            _mockRentalRepository.Verify(repo => repo.RentMovieById(rentDto), Times.Once);
        }

        /// <summary>
        ///  Negative Test: Renting a movie by ID with invalid data
        /// </summary>
        [Fact]
        public void RentMovieById_InvalidData_ThrowsInvalidOperationException()
        {
            // Arrange
            var rentDto = new RentMovieByID_DTO { MovieId = 1, CustomerId = 1 };
            _mockRentalRepository.Setup(repo => repo.RentMovieById(rentDto))
                .Throws(new InvalidOperationException("Invalid movie ID or customer ID."));

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _rentalService.RentMovieById(rentDto));
            Assert.Equal("Invalid movie ID or customer ID.", exception.Message);
        }

        /// <summary>
        /// Positive Test: Renting a movie by title successfully
        /// </summary>
        [Fact]
        public void RentMovieByTitle_ValidDto_Success()
        {
            // Arrange
            var rentDto = new RentMovieByName_DTO { Title = "Harry Potter and The Prisnor of Azkaban", Username = "khushi" };
            _mockRentalRepository.Setup(repo => repo.RentMovieByTitle(rentDto)).Verifiable();

            // Act
            _rentalService.RentMovieByTitle(rentDto);

            // Assert
            _mockRentalRepository.Verify(repo => repo.RentMovieByTitle(rentDto), Times.Once);
        }

        /// <summary>
        /// Negative Test: Renting a movie by title with invalid data
        /// </summary>
        [Fact]
        public void RentMovieByTitle_InvalidData_ThrowsInvalidOperationException()
        {
            // Arrange
            var rentDto = new RentMovieByName_DTO { Title = "Harry Potter and The Prisnor of Azkaban", Username = "khushi" };
            _mockRentalRepository.Setup(repo => repo.RentMovieByTitle(rentDto))
                .Throws(new InvalidOperationException("Invalid movie title or customer username."));

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _rentalService.RentMovieByTitle(rentDto));
            Assert.Equal("Invalid movie title or customer username.", exception.Message);
        }

        /// <summary>
        /// Positive Test: Get customers by movie ID
        /// </summary>
        [Fact]
        public void GetCustomersByMovieId_ValidMovieId_ReturnsCustomers()
        {
            // Arrange
            var movieId = 1;
            var customers = new List<CustomerResponseDTO>
            {
                new CustomerResponseDTO { Id = 1, Username = "khushi", Email = "khushi@example.com" },
                new CustomerResponseDTO { Id = 2, Username = "olivor", Email = "olivor@example.com" }
            };
            _mockRentalRepository.Setup(repo => repo.GetCustomersByMovieId(movieId)).Returns(customers);

            // Act
            var result = _rentalService.GetCustomersByMovieId(movieId);

            // Assert
            Assert.Equal(customers.Count, result.Count());
            Assert.Equal(customers, result);
        }

        /// <summary>
        /// Negative Test: Get customers by movie ID with invalid ID
        /// </summary>
        [Fact]
        public void GetCustomersByMovieId_InvalidMovieId_ReturnsEmpty()
        {
            // Arrange
            var movieId = 999; // Assuming 999 is an invalid ID
            _mockRentalRepository.Setup(repo => repo.GetCustomersByMovieId(movieId)).Returns(new List<CustomerResponseDTO>());

            // Act
            var result = _rentalService.GetCustomersByMovieId(movieId);

            // Assert
            Assert.Empty(result);
        }

        /// <summary>
        /// Positive Test: Get movies by customer ID
        /// </summary>
        [Fact]
        public void GetMoviesByCustomerId_ValidCustomerId_ReturnsMovies()
        {
            // Arrange
            var customerId = 1;
            var movies = new List<MovieResponseDTO>
            {
                new MovieResponseDTO { Id = 1, Title = "Harry Potter and The Prisnor of Azkaban", Price = 100 },
                new MovieResponseDTO { Id = 2, Title = "Harry Potter and The Deathly Hallows", Price = 150 }
            };
            _mockRentalRepository.Setup(repo => repo.GetMoviesByCustomerId(customerId)).Returns(movies);

            // Act
            var result = _rentalService.GetMoviesByCustomerId(customerId);

            // Assert
            Assert.Equal(movies.Count, result.Count());
            Assert.Equal(movies, result);
        }

        /// <summary>
        /// Negative Test: Get movies by customer ID with invalid ID
        /// </summary>
        [Fact]
        public void GetMoviesByCustomerId_InvalidCustomerId_ReturnsEmpty()
        {
            // Arrange
            var customerId = 999; // Assuming 999 is an invalid ID
            _mockRentalRepository.Setup(repo => repo.GetMoviesByCustomerId(customerId)).Returns(new List<MovieResponseDTO>());

            // Act
            var result = _rentalService.GetMoviesByCustomerId(customerId);

            // Assert
            Assert.Empty(result);
        }

        /// <summary>
        /// Positive Test: Get total cost by customer ID
        /// </summary>
        [Fact]
        public void GetTotalCostByCustomerId_ValidCustomerId_ReturnsTotalCost()
        {
            // Arrange
            var customerId = 1;
            var totalCost = 50;
            _mockRentalRepository.Setup(repo => repo.GetTotalCostByCustomerId(customerId)).Returns(totalCost);

            // Act
            var result = _rentalService.GetTotalCostByCustomerId(customerId);

            // Assert
            Assert.Equal(totalCost, result);
        }

        /// <summary>
        /// Negative Test: Get total cost by customer ID with invalid ID
        /// </summary>
        [Fact]
        public void GetTotalCostByCustomerId_InvalidCustomerId_ReturnsZero()
        {
            // Arrange
            var customerId = 999; // Assuming 999 is an invalid ID
            _mockRentalRepository.Setup(repo => repo.GetTotalCostByCustomerId(customerId)).Returns(0);

            // Act
            var result = _rentalService.GetTotalCostByCustomerId(customerId);

            // Assert
            Assert.Equal(0, result);
        }
    }
}

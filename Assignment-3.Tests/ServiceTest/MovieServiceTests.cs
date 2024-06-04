using Assignment_3.DTO.RequestDTO;
using Assignment_3.DTO.ResponseDTO;
using Assignment_3.Respository.IRepository;
using Assignment_3.Service;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

/// <summary>
/// Unit tests for the MovieService class.
/// </summary>
namespace Assignment_3.Tests
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly IMovieService _movieService;

        /// <summary>
        /// Initializes a new instance of the MovieServiceTests class.
        /// </summary>
        public MovieServiceTests()
        {
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _movieService = new MovieService(_movieRepositoryMock.Object);
        }

        /// <summary>
        /// Tests the AddMovie method when a movie is added successfully.
        /// </summary>
        [Fact]
        public void AddMovie_ShouldReturnMovieId_WhenMovieIsAddedSuccessfully()
        {
            // Arrange
            var addMovieDto = new AddMovieDTO { Title = "Dune", Price = 120 };
            _movieRepositoryMock.Setup(repo => repo.AddMovie(addMovieDto)).Returns(1);

            // Act
            var result = _movieService.AddMovie(addMovieDto);

            // Assert
            Assert.Equal(1, result);
        }

        /// <summary>
        /// Tests the AddMovie method when a movie with a duplicate title already exists.
        /// </summary>
        [Fact]
        public void AddMovie_ShouldThrowInvalidOperationException_WhenMovieTitleAlreadyExists()
        {
            // Arrange
            var addMovieDto = new AddMovieDTO { Title = "Dune", Price = 120 };
            _movieRepositoryMock.Setup(repo => repo.AddMovie(addMovieDto))
                .Throws(new InvalidOperationException("Movie with the same title already exists."));

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _movieService.AddMovie(addMovieDto));
            Assert.Equal("Movie with the same title already exists.", exception.Message);
        }

        /// <summary>
        /// Tests the GetAllMovies method when movies exist.
        /// </summary>
        [Fact]
        public void GetAllMovies_ShouldReturnListOfMovies()
        {
            // Arrange
            var movies = new List<MovieResponseDTO>
            {
                new MovieResponseDTO { Id = 1, Title = "Martian", Price = 100 },
                new MovieResponseDTO { Id = 2, Title = "The Hobbit", Price = 150 }
            };
            _movieRepositoryMock.Setup(repo => repo.GetAllMovies()).Returns(movies);

            // Act
            var result = _movieService.GetAllMovies();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, m => m.Title == "Martian");
            Assert.Contains(result, m => m.Title == "The Hobbit");
        }

        /// <summary>
        /// Tests the GetAllMovies method when no movies exist.
        /// </summary>
        [Fact]
        public void GetAllMovies_ShouldReturnEmptyList_WhenNoMoviesExist()
        {
            // Arrange
            var movies = new List<MovieResponseDTO>();
            _movieRepositoryMock.Setup(repo => repo.GetAllMovies()).Returns(movies);

            // Act
            var result = _movieService.GetAllMovies();

            // Assert
            Assert.Empty(result);
        }

        /// <summary>
        /// Tests the GetMovieById method when a movie with the specified ID exists.
        /// </summary>
        [Fact]
        public void GetMovieById_ShouldReturnMovie_WhenMovieExists()
        {
            // Arrange
            var movieId = 1;
            var movie = new MovieResponseDTO { Id = 1, Title = "Dune", Price = 120 };
            _movieRepositoryMock.Setup(repo => repo.GetMovieById(movieId)).Returns(movie);

            // Act
            var result = _movieService.GetMovieById(movieId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movieId, result.Id);
            Assert.Equal("Dune", result.Title);
        }

        /// <summary>
        /// Tests the GetMovieById method when no movie with the specified ID exists.
        /// </summary>
        [Fact]
        public void GetMovieById_ShouldReturnNull_WhenMovieDoesNotExist()
        {
            // Arrange
            var movieId = 1;
            _movieRepositoryMock.Setup(repo => repo.GetMovieById(movieId)).Returns((MovieResponseDTO)null);

            // Act
            var result = _movieService.GetMovieById(movieId);

            // Assert
            Assert.Null(result);
        }
        /// <summary>
        /// Tests the GetMovieByTitle method when a movie with the specified title exists.
        /// </summary>
        [Fact]
        public void GetMovieByTitle_ShouldReturnMovie_WhenMovieExists()
        {
            // Arrange
            var title = "Dune";
            var movie = new MovieResponseDTO { Id = 1, Title = title, Price = 120 };
            _movieRepositoryMock.Setup(repo => repo.GetMovieByTitle(title)).Returns(movie);

            // Act
            var result = _movieService.GetMovieByTitle(title);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(title, result.Title);
        }

        /// <summary>
        /// Tests the GetMovieByTitle method when no movie with the specified title exists.
        /// </summary>
        [Fact]
        public void GetMovieByTitle_ShouldReturnNull_WhenMovieDoesNotExist()
        {
            // Arrange
            var title = "Dune";
            _movieRepositoryMock.Setup(repo => repo.GetMovieByTitle(title)).Returns((MovieResponseDTO)null);

            // Act
            var result = _movieService.GetMovieByTitle(title);

            // Assert
            Assert.Null(result);
        }
    }
}

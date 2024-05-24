using Assignment_2.CustomExceptions;
using Assignment_2.DTO;
using Assignment_2.Repository;
using Assignment_2.Services;
using Moq;
using Xunit;

namespace Assignment_2.Tests
{
    /// <summary>
    /// Test class for UserService.
    /// </summary>
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserService _userService;

        /// <summary>
        /// Constructor for UserServiceTests.
        /// </summary>
        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        /// <summary>
        /// Test method to verify if user is registered successfully when email and username are unique.
        /// </summary>
        [Fact]
        public void RegisterUser_ShouldRegisterUser_WhenEmailAndUsernameAreUnique()
        {
            // Arrange
            var userDto = new UserDTO
            {
                Username = "khushi",
                Email = "khushi@example.com",
                Password = "Khushi@123",
                Name = "Khushi Rani",
                Address = "Kengeri, Bangalore",
                PhoneNumber = "1234567890",
                Role = "User"
            };

            _mockUserRepository.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).Returns((UserModel)null);
            _mockUserRepository.Setup(repo => repo.GetUserByUsername(It.IsAny<string>())).Returns((UserModel)null);

            // Act
            var result = _userService.RegisterUser(userDto);

            // Assert
            Assert.Equal("User registered successfully", result);
            _mockUserRepository.Verify(repo => repo.AddUser(It.IsAny<UserModel>()), Times.Once);
        }

        /// <summary>
        /// Test method to verify if UniqueEmailException is thrown when email is already in use.
        /// </summary>
        [Fact]
        public void RegisterUser_ShouldThrowUniqueEmailException_WhenEmailIsAlreadyInUse()
        {
            // Arrange
            var userDto = new UserDTO
            {
                Username = "khushi",
                Email = "khushi@example.com",
                Password = "Khushi@123",
                Name = "Khushi Rani",
                Address = "Kengeri, Bangalore",
                PhoneNumber = "1234567890",
                Role = "User"
            };

            var existingUser = new UserModel
            {
                Username = "Harold",
                Email = "khushi@example.com"
            };

            _mockUserRepository.Setup(repo => repo.GetUserByEmail(userDto.Email)).Returns(existingUser);

            // Act & Assert
            var exception = Assert.Throws<UniqueEmailException>(() => _userService.RegisterUser(userDto));
            Assert.Equal("Email already in use", exception.Message);
        }

        /// <summary>
        /// Test method to verify if UniqueUsernameException is thrown when username is already in use.
        /// </summary>
        [Fact]
        public void RegisterUser_ShouldThrowUniqueUsernameException_WhenUsernameIsAlreadyInUse()
        {
            // Arrange
            var userDto = new UserDTO
            {
                Username = "khushi",
                Email = "khushi@example.com",
                Password = "Khushi@123",
                Name = "Khushi Rani",
                Address = "Kengeri, Bangalore",
                PhoneNumber = "1234567890",
                Role = "User"
            };

            var existingUser = new UserModel
            {
                Username = "khushi",
                Email = "harold@example.com"
            };

            _mockUserRepository.Setup(repo => repo.GetUserByUsername(userDto.Username)).Returns(existingUser);

            // Act & Assert
            var exception = Assert.Throws<UniqueUsernameException>(() => _userService.RegisterUser(userDto));
            Assert.Equal("Username already in use", exception.Message);
        }

        /// <summary>
        /// Test method to verify if user details are returned when the user exists.
        /// </summary>
        [Fact]
        public void GetUserByUsername_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var existingUser = new UserModel
            {
                Username = "khushi",
                Email = "khushi@example.com"
            };

            _mockUserRepository.Setup(repo => repo.GetUserByUsername(existingUser.Username)).Returns(existingUser);

            // Act
            var result = _userService.GetUserByUsername(existingUser.Username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingUser.Username, result.Username);
        }

        /// <summary>
        /// Test method to verify if null is returned when the user does not exist.
        /// </summary>
        [Fact]
        public void GetUserByUsername_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetUserByUsername(It.IsAny<string>())).Returns((UserModel)null);

            // Act
            var result = _userService.GetUserByUsername("jughead");

            // Assert
            Assert.Null(result);
        }
    }
}

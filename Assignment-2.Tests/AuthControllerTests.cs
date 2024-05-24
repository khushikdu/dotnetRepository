using Assignment_2.Controllers;
using Assignment_2.CustomExceptions;
using Assignment_2.DTO;
using Assignment_2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Assignment_2.Tests
{
    /// <summary>
    /// Test class for AuthController.
    /// </summary>
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly AuthController _authController;

        /// <summary>
        /// Constructor for AuthControllerTests.
        /// </summary>
        public AuthControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockConfig = new Mock<IConfiguration>();

            // Set up mock configuration for JWT
            _mockConfig.Setup(config => config["Jwt:Key"]).Returns("ThisIsASecretKeyForJwtToken");
            _mockConfig.Setup(config => config["Jwt:Issuer"]).Returns("TestIssuer");

            // Instantiate AuthController with mock dependencies
            _authController = new AuthController(_mockConfig.Object, _mockUserService.Object);
        }

        /// <summary>
        /// Test method to verify if JWT token is returned when credentials are valid.
        /// </summary>
        [Fact]
        public void Post_ShouldReturnJwtToken_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginUser { Username = "khushi", Password = "Khushi@123" };
            var user = new UserModel { Username = "khushi", Password = "Khushi@123", Role = "User" };

            _mockUserService.Setup(service => service.GetUserByUsername(loginRequest.Username)).Returns(user);

            // Act
            var result = _authController.Post(loginRequest) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result.Value);
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.ReadToken(result.Value.ToString()) as JwtSecurityToken;
            Assert.NotNull(token);
            Assert.Contains(token.Claims, claim => claim.Type == ClaimTypes.Name && claim.Value == loginRequest.Username);
        }

        /// <summary>
        /// Test method to verify if unauthorized status is returned when username is invalid.
        /// </summary>
        [Fact]
        public void Post_ShouldReturnUnauthorized_WhenUsernameIsInvalid()
        {
            // Arrange
            var loginRequest = new LoginUser { Username = "harold", Password = "SomePassword1!" };

            _mockUserService.Setup(service => service.GetUserByUsername(loginRequest.Username)).Returns((UserModel)null);

            // Act
            var result = _authController.Post(loginRequest) as UnauthorizedObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid username or password", result.Value);
        }

        /// <summary>
        /// Test method to verify if unauthorized status is returned when password is incorrect.
        /// </summary>
        [Fact]
        public void Post_ShouldReturnUnauthorized_WhenPasswordIsIncorrect()
        {
            // Arrange
            var loginRequest = new LoginUser { Username = "khushi", Password = "InvalidPassword!" };
            var user = new UserModel { Username = "khushi", Password = "Khushi@123", Role = "User" };

            _mockUserService.Setup(service => service.GetUserByUsername(loginRequest.Username)).Returns(user);

            // Act
            var result = _authController.Post(loginRequest) as UnauthorizedObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Incorrect password", result.Value);
        }

        /// <summary>
        /// Test method to verify if user registration returns OK status when successful.
        /// </summary>
        [Fact]
        public void Register_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var userDto = new UserDTO
            {
                Username = "betty",
                Email = "betty@example.com",
                Password = "Password1!",
                Name = "Betty Cooper",
                Address = "Riverdale",
                PhoneNumber = "1234567890",
                Role = "User"
            };

            _mockUserService.Setup(service => service.RegisterUser(userDto)).Returns("User registered successfully");

            // Act
            var result = _authController.Register(userDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User registered successfully", result.Value);
        }

        /// <summary>
        /// Test method to verify if bad request status is returned when model state is invalid during registration.
        /// </summary>
        [Fact]
        public void Register_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var userDto = new UserDTO
            {
                Username = "",
                Email = "invalidEmail",
                Password = "short",
                Name = "Test User",
                Address = "Riverdale",
                PhoneNumber = "1234567890",
                Role = "User"
            };

            _authController.ModelState.AddModelError("Username", "Required");

            // Act
            var result = _authController.Register(userDto) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Test method to verify if bad request status is returned when email is already in use during registration.
        /// </summary>
        [Fact]
        public void Register_ShouldReturnBadRequest_WhenEmailIsAlreadyInUse()
        {
            // Arrange
            var userDto = new UserDTO
            {
                Username = "Hal",
                Email = "betty@example.com",
                Password = "Password1!",
                Name = "Test User",
                Address = "Riverdale",
                PhoneNumber = "1234567890",
                Role = "User"
            };

            _mockUserService.Setup(service => service.RegisterUser(userDto)).Throws(new UniqueEmailException("Email already in use"));

            // Act
            var result = _authController.Register(userDto) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email already in use", result.Value);
        }

        /// <summary>
        /// Test method to verify if bad request status is returned when username is already in use during registration.
        /// </summary>
        [Fact]
        public void Register_ShouldReturnBadRequest_WhenUsernameIsAlreadyInUse()
        {
            // Arrange
            var userDto = new UserDTO
            {
                Username = "Hal",
                Email = "new@example.com",
                Password = "Password1!",
                Name = "Test User",
                Address = "Riverdale",
                PhoneNumber = "1234567890",
                Role = "User"
            };

            _mockUserService.Setup(service => service.RegisterUser(userDto)).Throws(new UniqueUsernameException("Username already in use"));

            // Act
            var result = _authController.Register(userDto) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Username already in use", result.Value);
        }
    }
}

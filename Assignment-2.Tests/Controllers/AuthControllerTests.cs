using Assignment_2.Controllers;
using Assignment_2.CustomExceptions;
using Assignment_2.DTO;
using Assignment_2.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Assignment_2.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockAuthService = new Mock<IAuthService>();
            _mockConfig = new Mock<IConfiguration>();
            _authController = new AuthController(_mockConfig.Object, _mockUserService.Object, _mockAuthService.Object);
        }

        [Theory]
        [InlineData("testuser", "testpassword")]
        public void Post_ShouldReturnToken_WhenCredentialsAreValid(string username, string password)
        {
            // Arrange
            var loginUser = new LoginUser { Username = username, Password = password };
            var token = "generated-jwt-token";
            _mockAuthService.Setup(service => service.Authenticate(loginUser)).Returns(token);

            // Act
            var result = _authController.Post(loginUser) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(token, result.Value);
        }


        [Theory]
        [InlineData("testuser", "testpassword")]
        public void Post_ShouldReturnOk_WhenCredentialsAreValid(string username, string password)
        {
            // Arrange
            var loginRequest = new LoginUser { Username = username, Password = password };
            var token = "validToken";
            _mockAuthService.Setup(service => service.Authenticate(loginRequest)).Returns(token);

            // Act
            var result = _authController.Post(loginRequest) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(token, result.Value);
        }

        [Theory]
        [InlineData("newuser", "newuser@example.com", "newpassword", "New User", "New Address", "1234567890", "User")]
        public void Register_ShouldReturnSuccessMessage_WhenUserIsRegisteredSuccessfully(string username, string email, string password, string name, string address, string phoneNumber, string role)
        {
            // Arrange
            var userDto = new UserDTO
            {
                Username = username,
                Email = email,
                Password = password,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                Role = role
            };
            var expectedResult = "User registered successfully";
            _mockUserService.Setup(service => service.RegisterUser(userDto)).Returns(expectedResult);

            // Act
            var result = _authController.Register(userDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, result.Value);
        }

    }
}

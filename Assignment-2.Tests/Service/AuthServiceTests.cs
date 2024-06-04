using Assignment_2.CustomExceptions;
using Assignment_2.DTO;
using Assignment_2.Repository.Interface;
using Assignment_2.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Assignment_2.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _mockAuthRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _mockAuthRepository = new Mock<IAuthRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(config => config["Jwt:Key"]).Returns("supersecretkey12345");
            _mockConfig.Setup(config => config["Jwt:Issuer"]).Returns("testissuer");

            _authService = new AuthService(_mockAuthRepository.Object, _mockConfig.Object, _mockUserRepository.Object);
        }

        [Fact]
        public void Authenticate_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var username = "testuser";
            var password = "correctpassword";
            var user = new UserModel { Username = username, Password = password, Role = "User" };
            _mockUserRepository.Setup(repo => repo.GetUserByUsername(username)).Returns(user);

            // Act
            var token = _authService.Authenticate(new LoginUser { Username = username, Password = password });

            // Assert
            Assert.NotNull(token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey12345"));
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "testissuer",
                ValidAudience = "testissuer",
                IssuerSigningKey = securityKey
            };

            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            Assert.NotNull(validatedToken);
        }

        [Fact]
        public void Authenticate_ShouldThrowInvalidCredentialsException_WhenUserNotFound()
        {
            // Arrange
            var username = "nonexistentuser";
            var password = "password";
            _mockUserRepository.Setup(repo => repo.GetUserByUsername(username)).Returns((UserModel)null);

            // Act & Assert
            var exception = Assert.Throws<InvalidCredentialsException>(() =>
                _authService.Authenticate(new LoginUser { Username = username, Password = password }));
            Assert.Equal("Invalid username or password", exception.Message);
        }

        [Fact]
        public void Authenticate_ShouldThrowInvalidCredentialsException_WhenPasswordIsIncorrect()
        {
            // Arrange
            var username = "testuser";
            var password = "wrongpassword";
            var user = new UserModel { Username = username, Password = "correctpassword", Role = "User" };
            _mockUserRepository.Setup(repo => repo.GetUserByUsername(username)).Returns(user);

            // Act & Assert
            var exception = Assert.Throws<InvalidCredentialsException>(() =>
                _authService.Authenticate(new LoginUser { Username = username, Password = password }));
            Assert.Equal("Incorrect password", exception.Message);
        }

        [Fact]
        public void Authenticate_ShouldThrowInvalidCredentialsException_WhenUsernameOrPasswordIsEmpty()
        {
            // Arrange
            var username = "";
            var password = "password";

            // Act & Assert
            var exception = Assert.Throws<InvalidCredentialsException>(() =>
                _authService.Authenticate(new LoginUser { Username = username, Password = password }));
            Assert.Equal("Invalid username or password", exception.Message);
        }

        [Fact]
        public void Authenticate_ShouldThrowInvalidCredentialsException_WhenUsernameOrPasswordIsNull()
        {
            // Arrange
            var username = "testuser";
            string password = null;

            // Act & Assert
            var exception = Assert.Throws<InvalidCredentialsException>(() =>
                _authService.Authenticate(new LoginUser { Username = username, Password = password }));
            Assert.Equal("Invalid username or password", exception.Message);
        }
    }
}

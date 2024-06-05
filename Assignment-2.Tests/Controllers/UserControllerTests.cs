using Assignment_2.DTO;
using Assignment_2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Http;
using Assignment_2.Services.Interface;

namespace Assignment_2.Tests.Controllers
{
    /// <summary>
    /// Test class for UserController.
    /// </summary>
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;

        /// <summary>
        /// Constructor for UserControllerTests.
        /// </summary>
        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }

        /// <summary>
        /// Test method to verify if user details are returned when the user is found.
        /// </summary>
        [Fact]
        public void GetAuthenticated_ShouldReturnUserDetails_WhenUserIsFound()
        {
            // Arrange
            var username = "khushi";
            var user = new UserModel
            {
                Username = username,
                Email = "khushi@example.com",
                Name = "Khushi",
                Address = "Kengeri, Bangalore",
                PhoneNumber = "1234567890",
                Role = "User"
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _mockUserService.Setup(service => service.GetUserByUsername(username)).Returns(user);

            _userController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = _userController.GetAuthenticated() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(user, result.Value);
        }

        /// <summary>
        /// Test method to verify if not found status is returned when the user is not found.
        /// </summary>
        [Fact]
        public void GetAuthenticated_ShouldReturnNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var username = "khushi";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _mockUserService.Setup(service => service.GetUserByUsername(username)).Returns((UserModel)null);

            _userController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            // Act
            var result = _userController.GetAuthenticated() as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", result.Value);
        }
    }
}

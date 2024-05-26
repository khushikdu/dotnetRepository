using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Assignment_3.Controllers;
using Assignment_3.DTO.RequestDTO;
using Assignment_3.Services;
using System;
using System.Collections.Generic;

namespace Assignment_3.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="CustomersController"/> class.
    /// </summary>
    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomersController _customersController;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersControllerTests"/> class.
        /// </summary>
        public CustomersControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _customersController = new CustomersController(_mockCustomerService.Object);
        }

        /// <summary>
        /// Tests if adding a valid customer returns an OK result with the expected customer ID.
        /// </summary>
        /// <param name="username">The username of the customer.</param>
        /// <param name="email">The email of the customer.</param>
        /// <param name="expectedCustomerId">The expected customer ID returned by the service.</param>
        [Theory]
        [InlineData("khushi", "khushi@example.com", 1)]
        [InlineData("olivor", "olivor@example.com", 2)]
        public void AddCustomer_ValidCustomer_ReturnsOkResult(string username, string email, int expectedCustomerId)
        {
            // Arrange
            var customerDto = new AddCustomerDTO { Username = username, Email = email };
            _mockCustomerService.Setup(service => service.AddCustomer(customerDto)).Returns(expectedCustomerId);

            // Act
            var result = _customersController.AddCustomer(customerDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(expectedCustomerId, okResult.Value);
        }

        /// <summary>
        /// Tests if adding a customer with a duplicate email throws an <see cref="InvalidOperationException"/> and returns a conflict result.
        /// </summary>
        /// <param name="username">The username of the customer.</param>
        /// <param name="email">The email of the customer.</param>
        [Theory]
        [InlineData("khushi", "khushi@example.com")]
        [InlineData("olivor", "olivor@example.com")]
        public void AddCustomer_DuplicateEmail_ThrowsInvalidOperationException(string username, string email)
        {
            // Arrange
            var customerDto = new AddCustomerDTO { Username = username, Email = email };
            _mockCustomerService.Setup(service => service.AddCustomer(customerDto))
                .Throws(new InvalidOperationException("Customer with the same email already exists."));

            // Act
            var result = _customersController.AddCustomer(customerDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var conflictResult = Assert.IsType<ConflictObjectResult>(actionResult.Result);
            Assert.Equal("Customer with the same email already exists.", conflictResult.Value);
        }

        /// <summary>
        /// Provides data for testing the addition of valid customers.
        /// </summary>
        /// <returns>A collection of test data.</returns>
        public static IEnumerable<object[]> GetCustomerData()
        {
            yield return new object[] { new AddCustomerDTO { Username = "khushi", Email = "khushi@example.com" }, 1 };
            yield return new object[] { new AddCustomerDTO { Username = "olivor", Email = "olivor@example.com" }, 2 };
        }

        /// <summary>
        /// Tests if adding a valid customer using member data returns an OK result with the expected customer ID.
        /// </summary>
        /// <param name="customerDto">The data transfer object containing customer information.</param>
        /// <param name="expectedCustomerId">The expected customer ID returned by the service.</param>
        [Theory]
        [MemberData(nameof(GetCustomerData))]
        public void AddCustomer_ValidCustomer_ReturnsOkResult_MemberData(AddCustomerDTO customerDto, int expectedCustomerId)
        {
            // Arrange
            _mockCustomerService.Setup(service => service.AddCustomer(customerDto)).Returns(expectedCustomerId);

            // Act
            var result = _customersController.AddCustomer(customerDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(expectedCustomerId, okResult.Value);
        }

        /// <summary>
        /// Provides data for testing the addition of customers with duplicate emails.
        /// </summary>
        /// <returns>A collection of test data.</returns>
        public static IEnumerable<object[]> GetDuplicateCustomerData()
        {
            yield return new object[] { new AddCustomerDTO { Username = "khushi", Email = "khushi@example.com" } };
            yield return new object[] { new AddCustomerDTO { Username = "olivor", Email = "olivor@example.com" } };
        }

        /// <summary>
        /// Tests if adding a customer with a duplicate email using member data throws an <see cref="InvalidOperationException"/> and returns a conflict result.
        /// </summary>
        /// <param name="customerDto">The data transfer object containing customer information.</param>
        [Theory]
        [MemberData(nameof(GetDuplicateCustomerData))]
        public void AddCustomer_DuplicateEmail_ThrowsInvalidOperationException_MemberData(AddCustomerDTO customerDto)
        {
            // Arrange
            _mockCustomerService.Setup(service => service.AddCustomer(customerDto))
                .Throws(new InvalidOperationException("Customer with the same email already exists."));

            // Act
            var result = _customersController.AddCustomer(customerDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var conflictResult = Assert.IsType<ConflictObjectResult>(actionResult.Result);
            Assert.Equal("Customer with the same email already exists.", conflictResult.Value);
        }
    }
}

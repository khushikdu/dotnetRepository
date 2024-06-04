using Xunit;
using Moq;
using Assignment_3.Respository.IRepository;
using Assignment_3.DTO.RequestDTO;
using System;
using Assignment_3.Services;

/// <summary>
/// Unit tests for the CustomerService class.
/// </summary>
public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _mockCustomerRepository;
    private readonly ICustomerService _customerService;

    /// <summary>
    /// Initializes a new instance of the CustomerServiceTests class.
    /// </summary>
    public CustomerServiceTests()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _customerService = new CustomerService(_mockCustomerRepository.Object);
    }

    /// <summary>
    /// Tests the AddCustomer method with valid customer data.
    /// </summary>
    [Fact]
    public void AddCustomer_ValidCustomer_ReturnsCustomerId()
    {
        // Arrange
        var customerDto = new AddCustomerDTO { Username = "khushi", Email = "khushi@example.com" };
        _mockCustomerRepository.Setup(repo => repo.AddCustomer(customerDto)).Returns(1);

        // Act
        var result = _customerService.AddCustomer(customerDto);

        // Assert
        Assert.Equal(1, result);
    }

    /// <summary>
    /// Tests the AddCustomer method with duplicate email.
    /// </summary>
    [Fact]
    public void AddCustomer_DuplicateEmail_ThrowsInvalidOperationException()
    {
        // Arrange
        var customerDto = new AddCustomerDTO { Username = "khushi", Email = "khushi@example.com" };
        _mockCustomerRepository.Setup(repo => repo.AddCustomer(customerDto))
            .Throws(new InvalidOperationException("Customer with the same email already exists."));

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _customerService.AddCustomer(customerDto));
        Assert.Equal("Customer with the same email already exists.", exception.Message);
    }
}

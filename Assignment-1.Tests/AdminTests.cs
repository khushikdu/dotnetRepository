using Xunit;
using System;
using System.IO;
using System.Linq;
using Assignment_1.Enums;
using Assignment_1.Utils;

namespace Assignment_1.Tests
{
    /// <summary>
    /// Test class for testing the functionalities of the Library system.
    /// </summary>
    public class AdminTests
    {
        private readonly Library _library;

        /// <summary>
        /// Initializes a new instance of the <see cref="Assignment_1Tests"/> class.
        /// </summary>
        public AdminTests()
        {
            _library = new Library();
        }


        /// <summary>
        /// Tests if a new admin can be added to the library.
        /// </summary>
        [Fact]
        public void AddAdmin_ShouldAddNewAdmin()
        {
            // Arrange

            // Act
            var initialAdminCount = _library.admins.Count;
            var newAdmin = new Admin(2, "Jonathan", "1234");
            _library.admins.Add(newAdmin);

            // Assert
            Assert.Equal(initialAdminCount + 1, _library.admins.Count);
            Assert.Contains(newAdmin, _library.admins);
        }

        /// <summary>
        /// Tests if adding a duplicate admin throws an exception.
        /// </summary>
        [Fact]
        public void AddAdmin_ShouldThrowExceptionWhenAddingDuplicateAdmin()
        {
            // Arrange
            var existingAdmin = new Admin(1, "Admin", "1234");
            _library.admins.Add(existingAdmin);

            // Simulate user input
            var input = new StringReader("Admin\npassword\n");
            Console.SetIn(input);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _library.AddAdmin(_library));
            Assert.Equal("Admin with the same name already exists.", exception.Message);
        }

    }
}
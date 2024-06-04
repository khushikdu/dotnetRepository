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
    public class UserTests
    {
        private readonly Library _library;

        /// <summary>
        /// Initializes a new instance of the <see cref="Assignment_1Tests"/> class.
        /// </summary>
        public UserTests()
        {
            _library = new Library();
        }

        /// <summary>
        /// Tests if a new user can be added to the library.
        /// </summary>
        [Fact]
        public void AddUser_ShouldAddNewUser()
        {
            // Arrange
            int userId = 3;
            string userName = "Khushi";
            var userType = UserType.Teacher;

            // Act
            User user = new User(userId, userName, userType);
            _library.users.Add(user);

            // Assert
            var addedUser = _library.users.SingleOrDefault(u => u.UserId == userId);
            Assert.NotNull(addedUser);
            Assert.Equal(userName, addedUser.Name);
            Assert.Equal(userType, addedUser.Type);
        }
    }
}
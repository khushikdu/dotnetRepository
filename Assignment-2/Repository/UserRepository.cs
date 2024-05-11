using System.Collections.Generic;
using System.Linq;

namespace Assignment_2.Repository
{
    /// <summary>
    /// Repository class for managing user data.
    /// </summary>
    public class UserRepository
    {
        private List<UserModel> _users = new List<UserModel>();

        /// <summary>
        /// Adds a user to the repository.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public void AddUser(UserModel user)
        {
            _users.Add(user);
        }

        /// <summary>
        /// Retrieves a user by email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The user with the specified email address, or null if not found.</returns>
        public UserModel GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Retrieves a user by username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user with the specified username, or null if not found.</returns>
        public UserModel GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }
    }
}

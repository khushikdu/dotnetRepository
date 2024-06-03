using Assignment_2.DTO;
using Assignment_2.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_2.Repository
{
    /// <summary>
    /// Implementation of the authentication repository interface.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly List<UserModel> _users = new List<UserModel>();

        /// <summary>
        /// Adds a user to the repository.
        /// </summary>
        /// <param name="user">The user model to add.</param>
        public void AddUser(UserModel user)
        {
            _users.Add(user);
        }
    }
}

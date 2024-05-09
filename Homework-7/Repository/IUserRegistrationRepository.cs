using Homework_7.Entity;
using Homework_7.DTO;

namespace Homework_7.Repository
{
    /// <summary>
    /// Interface for user registration repository.
    /// </summary>
    public interface IUserRegistrationRepository
    {
        /// <summary>
        /// Saves the user registration information.
        /// </summary>
        /// <param name="user">User object containing registration details.</param>
        void Save(User user);

        /// <summary>
        /// Retrieves all registered users.
        /// </summary>
        /// <returns>A list of UserDTO objects representing the registered users.</returns>
        List<UserDTO> GetAllUsers();

    }
}

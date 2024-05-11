using Assignment_2.CustomExceptions;
using Assignment_2.DTO;
using Assignment_2.Repository;

namespace Assignment_2.Services
{
    /// <summary>
    /// Service class for user-related operations.
    /// </summary>
    public class UserService
    {
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the UserService class with the specified UserRepository dependency.
        /// </summary>
        /// <param name="userRepository">The UserRepository instance to use for user data access.</param>
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Registers a new user based on the provided user DTO.
        /// </summary>
        /// <param name="userDto">The user DTO containing registration information.</param>
        /// <returns>A string indicating the result of the registration process.</returns>
        public string RegisterUser(UserDTO userDto)
        {
            // Check if email is already in use
            if (_userRepository.GetUserByEmail(userDto.Email) != null)
            {
                throw new UniqueEmailException("Email already in use");
            }

            // Check if username is already in use
            if (_userRepository.GetUserByUsername(userDto.Username) != null)
            {
                throw new UniqueUsernameException("Username already in use");
            }

            // Create UserModel instance from UserDTO
            var user = new UserModel
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                Name = userDto.Name,
                Address = userDto.Address,
                PhoneNumber = userDto.PhoneNumber,
                Role = userDto.Role
            };

            // Add user to repository
            _userRepository.AddUser(user);

            return "User registered successfully";
        }

        /// <summary>
        /// Retrieves a user by username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user with the specified username, or null if not found.</returns>
        public UserModel GetUserByUsername(string username)
        {
            // Retrieve user from repository based on username
            return _userRepository.GetUserByUsername(username);
        }
    }
}

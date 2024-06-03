using Assignment_2.CustomExceptions;
using Assignment_2.DTO;
using Assignment_2.Mapper;
using Assignment_2.Repository.Interface;
using Assignment_2.Services.Interface;

namespace Assignment_2.Services
{
    /// <summary>
    /// Service class for user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the UserService class with the specified UserRepository dependency.
        /// </summary>
        /// <param name="userRepository">The UserRepository instance to use for user data access.</param>
        public UserService(IUserRepository userRepository)
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
            if (_userRepository.GetUserByUsername(userDto.Username) != null)
            {
                throw new UniqueUsernameException("Username already in use");
            }

            if (_userRepository.GetUserByEmail(userDto.Email) != null)
            {
                throw new UniqueEmailException("Email already in use");
            }


            UserModel user = MapUserDTOToModel.Map(userDto);

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
            return _userRepository.GetUserByUsername(username);
        }
    }
}

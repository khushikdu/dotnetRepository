using Assignment_2.CustomExceptions;
using Assignment_2.DTO;
using Assignment_2.Repository;

namespace Assignment_2.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string RegisterUser(UserDTO userDto)
        {
            if (_userRepository.GetUserByEmail(userDto.Email) != null)
            {
                throw new UniqueEmailException("Email already in use");
            }

            if (_userRepository.GetUserByUsername(userDto.Username) != null)
            {
                throw new UniqueUsernameException("Username already in use");
            }

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

            _userRepository.AddUser(user);

            return "User registered successfully";
        }
        public UserModel GetUserByUsername(string username)
        {
            // Retrieve user from repository based on username
            return _userRepository.GetUserByUsername(username);
        }
    }
}

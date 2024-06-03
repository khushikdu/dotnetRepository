using Homework_7.Entity;
using Homework_7.ViewModel;
using Homework_7.Mapper;
using Microsoft.Extensions.Logging;
using Homework_7.Service.Interface;

namespace Homework_7.Service
{
    /// <summary>
    /// Service class responsible for user registration.
    /// </summary>
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly ILogger<UserRegistrationService> _logger;
        public List<User> UserList;

        /// <summary>
        /// Constructor for UserRegistrationService.
        /// </summary>
        /// <param name="logger">Logger instance for logging.</param>
        public UserRegistrationService(ILogger<UserRegistrationService> logger)
        {
            UserList = new List<User>();
            _logger = logger;
        }

        /// <summary>
        /// Saves the user registration information.
        /// </summary>
        /// <param name="user">User object containing registration details.</param>
        public void Save(UserVM userVm)
        {
            UserMapper userMapper = new UserMapper();
            User user = userMapper.Map(userVm);
            UserList.Add(user);
            _logger.LogInformation("User registered successfully: {Username}", user.Username);
        }

        /// <summary>
        /// Retrieves all registered users.
        /// </summary>
        /// <returns>A list of UserDTO objects representing the registered users.</returns>
        public List<UserVM> GetAllUsers()
        {
            UserMapper userMapper = new UserMapper();
            List<UserVM> userDTOs = new List<UserVM>();
            foreach (var user in UserList)
            {
                userDTOs.Add(userMapper.Map(user)); 
            }
            return userDTOs;
        }
    }
}

using Homework_7.DTO;
using Homework_7.Entity;

namespace Homework_7.Mapper
{
    /// <summary>
    /// Mapper class for mapping UserDTO objects to User entities.
    /// </summary>
    public class UserMapper
    {
        /// <summary>
        /// Maps a UserDTO object to a User entity.
        /// </summary>
        /// <param name="userDto">UserDTO object to be mapped.</param>
        /// <returns>User entity mapped from the UserDTO object.</returns>
        public User Map(UserDTO userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                ConfirmPassword = userDto.ConfirmPassword,
                Age = userDto.Age,
                PhoneNumber = userDto.PhoneNumber,
                Country = userDto.Country,
                Gender = userDto.Gender,
                CreditCard = userDto.CreditCard,
                ExpirationDate = userDto.ExpirationDate,
                CVV = userDto.CVV
            };
            return user;
        }
        public UserDTO Map(User userDto)
        {
            var user = new UserDTO
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                ConfirmPassword = userDto.ConfirmPassword,
                Age = userDto.Age,
                PhoneNumber = userDto.PhoneNumber,
                Country = userDto.Country,
                Gender = userDto.Gender,
                CreditCard = userDto.CreditCard,
                ExpirationDate = userDto.ExpirationDate,
                CVV = userDto.CVV
            };
            return user;
        }
    }
}

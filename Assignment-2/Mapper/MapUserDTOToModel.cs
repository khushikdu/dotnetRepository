using Assignment_2.DTO;

namespace Assignment_2.Mapper
{
    public class MapUserDTOToModel
    {
        /// <summary>
        /// to map userDto to UserModel
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>User Model</returns>
        public static UserModel Map(UserDTO userDto)
        {
            return new UserModel
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                Name = userDto.Name,
                Address = userDto.Address,
                PhoneNumber = userDto.PhoneNumber,
                Role = userDto.Role
            };
        }
    }
}

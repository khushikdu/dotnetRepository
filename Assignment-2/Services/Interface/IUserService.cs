using Assignment_2.DTO;

namespace Assignment_2.Services.Interface
{
    public interface IUserService
    {
        string RegisterUser(UserDTO userDto);
        UserModel GetUserByUsername(string username);

    }
}

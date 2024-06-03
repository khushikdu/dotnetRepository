namespace Assignment_2.Repository.Interface
{
    public interface IUserRepository
    {
        void AddUser(UserModel user);
        UserModel GetUserByEmail(string email);
        UserModel GetUserByUsername(string username);
    }
}
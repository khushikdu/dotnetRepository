namespace Assignment_2.Repository
{
    public class UserRepository
    {
        private List<UserModel> _users = new List<UserModel>();

        public void AddUser(UserModel user)
        {
            _users.Add(user);
        }

        public UserModel GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }
        
        public UserModel GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }
    }
}

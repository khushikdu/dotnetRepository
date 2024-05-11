namespace Assignment_2.CustomExceptions
{
    public class UniqueUsernameException : GlobalException
    {
        public UniqueUsernameException(string message) : base(message)
        {
        }
    }
}

namespace Assessment_1.CustomExceptions
{
    public class UniqueEmailException: Exception
    {
        public UniqueEmailException(string message) : base(message)
        {
        }
    }
}

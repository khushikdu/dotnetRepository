namespace Assignment_2.CustomExceptions
{
    public class GlobalException  : Exception
    {
        public GlobalException(string message) : base(message) { }
    }
}
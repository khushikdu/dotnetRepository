namespace Assignment_2.CustomExceptions
{
    public class InvalidCredentialsException:GlobalException
    {
        public InvalidCredentialsException(string message):base(message)
        {
            
        }
    }
}

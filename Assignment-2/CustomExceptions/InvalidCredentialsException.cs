namespace Assignment_2.CustomExceptions
{
    /// <summary>
    /// Represents an exception that is thrown when invalid credentials are provided.
    /// </summary>
    public class InvalidCredentialsException : GlobalException
    {
        /// <summary>
        /// Initializes a new instance of the InvalidCredentialsException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidCredentialsException(string message) : base(message)
        {
        }
    }
}

namespace Assignment_2.CustomExceptions
{
    /// <summary>
    /// Represents a global exception that can be thrown within the application.
    /// </summary>
    public class GlobalException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the GlobalException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GlobalException(string message) : base(message) { }
    }
}

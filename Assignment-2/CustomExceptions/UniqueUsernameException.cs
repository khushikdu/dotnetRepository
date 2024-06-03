namespace Assignment_2.CustomExceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an attempt is made to register a user with a username that is already in use.
    /// </summary>
    public class UniqueUsernameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UniqueUsernameException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UniqueUsernameException(string message) : base(message)
        {
        }
    }
}

namespace Assignment_2.CustomExceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an attempt is made to register a user with an email address that is already in use.
    /// </summary>
    public class UniqueEmailException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UniqueEmailException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UniqueEmailException(string message) : base(message)
        {
        }
    }
}

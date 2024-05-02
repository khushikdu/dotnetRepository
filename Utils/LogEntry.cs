using System;

namespace Assignment_1.Utils
{
    /// <summary>
    /// Represents a log entry in the library system.
    /// </summary>
    internal class LogEntry
    {
        /// <summary>
        /// Gets or sets the timestamp of the log entry.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the action performed in the log entry.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the username associated with the log entry.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the title of the book associated with the log entry.
        /// </summary>
        public string BookTitle { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Assignment_1.Utils
{
    /// <summary>
    /// Manages logging of system actions.
    /// </summary>
    internal class LogManager
    {
        private List<LogEntry> logEntries = new List<LogEntry>();

        /// <summary>
        /// Adds a log entry to the log manager.
        /// </summary>
        /// <param name="entry">The log entry to add.</param>
        public void AddLogEntry(LogEntry entry)
        {
            logEntries.Add(entry);
        }

        /// <summary>
        /// Retrieves all log entries stored in the log manager.
        /// </summary>
        /// <returns>An enumerable collection of log entries.</returns>
        public IEnumerable<LogEntry> GetLogEntries()
        {
            return logEntries;
        }

        /// <summary>
        /// Prints all log entries to the console.
        /// </summary>
        public void PrintLogs()
        {
            foreach (var entry in logEntries)
            {
                Console.WriteLine($"{entry.Timestamp}, Action: {entry.Action} {entry.UserName} {entry.BookTitle}");
            }
        }
    }
}

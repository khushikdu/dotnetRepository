using System;


namespace Assignment_1.Utils
{
    internal class LogManager
    {
        private List<LogEntry> logEntries = new List<LogEntry>();
        public void AddLogEntry(LogEntry entry)
        {
            logEntries.Add(entry);
        }

        public IEnumerable<LogEntry> GetLogEntries()
        {
            return logEntries;
        }
        public void PrintLogs()
        {
            foreach (var entry in logEntries)
            {
                Console.WriteLine($"{entry.Timestamp}, Action: {entry.Action}, {entry.UserName}, {entry.BookTitle}");
            }
        }
    }

}

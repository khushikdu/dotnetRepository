using System;

namespace Homework3
{
    /// <summary>
    /// Represents a logger for logging messages to a file.
    /// </summary>
    class Logger : IDisposable
    {
        private static Logger instance;
        private string _logFilePath;
        private StreamWriter _streamWriter;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the Logger class with the specified log file path.
        /// </summary>
        /// <param name="logFilePath">The path of the log file.</param>
        private Logger(string logFilePath)
        {
            this._logFilePath = string.IsNullOrWhiteSpace(logFilePath) ? "log.txt" : logFilePath;
            this._streamWriter = File.AppendText(_logFilePath);
        }

        /// <summary>
        /// Gets the singleton instance of the Logger class.
        /// </summary>
        public static Logger Instance
        {
            get
            {
                if(instance==null)
                {
                    instance = new Logger("log.txt");
                }
                return instance;
            }
        }

        /// <summary>
        /// Logs a message with the current timestamp.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        private void Log(string message)
        {
            string directory = Path.GetDirectoryName(_logFilePath);
            if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory)) 
            {
                Directory.CreateDirectory(directory);
            }
            _streamWriter.WriteLine($"{DateTime.Now} : {message}");   
        }

        /// <summary>
        /// Disposes the logger and releases any resources used.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the logger and releases any resources used.
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether to dispose managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _streamWriter?.Close();
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Finalizes the logger instance.
        /// </summary>
        ~Logger()
        {
            Dispose(false);
        }

        /// <summary>
        /// Main method to log a message.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine($"Enter message to be logged : ");
            string message =Console.ReadLine();
            Logger.Instance.Log(message);
            Console.WriteLine("Message logged successfully");
            Logger.Instance.Dispose();
            Console.WriteLine("Disposed status: " + Logger.Instance.IsDisposed());
            Console.ReadKey();
        }

        /// <summary>
        /// Checks if the logger has been disposed.
        /// </summary>
        /// <returns>True if the logger has been disposed; otherwise, false.</returns>
        public bool IsDisposed()
        {
            return disposed;
        }
    }
}
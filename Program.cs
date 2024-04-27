using System;

namespace Homework3
{
    class Logger : IDisposable
    {
        private static Logger instance;
        private string _logFilePath;
        private StreamWriter _streamWriter;
        private bool disposed = false;

        private Logger(string logFilePath)
        {
            this._logFilePath = string.IsNullOrWhiteSpace(logFilePath) ? "log.txt" : logFilePath;
            this._streamWriter = File.AppendText(_logFilePath);
        }

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

        private void Log(string message)
        {
            string directory = Path.GetDirectoryName(_logFilePath);
            if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory)) 
            {
                Directory.CreateDirectory(directory);
            }
            _streamWriter.WriteLine($"{DateTime.Now} : {message}");   
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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

        ~Logger()
        {
            Dispose(false);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine($"Enter message to be logged : ");
            string message =Console.ReadLine();
            Logger.Instance.Log(message);
            Console.WriteLine("Message logged successfully");
            Logger.Instance.Dispose();
            Console.WriteLine("Disposed status: " + Logger.Instance.IsDisposed());
            //Console.ReadKey();
        }

        public bool IsDisposed()
        {
            return disposed;
        }
    }
}
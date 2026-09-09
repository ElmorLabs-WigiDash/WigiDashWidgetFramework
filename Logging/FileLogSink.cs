using System;
using System.IO;
using System.Text;

namespace WigiDashWidgetFramework.Logging
{
    // Default ILogSink: appends plain-text lines to a single log file. This is the built-in
    // replacement for the previous NLog FileTarget - swap in a different ILogSink (e.g. one
    // backed by NLog, Serilog, or a custom implementation) via LogManager.SetSink() instead.
    public sealed class FileLogSink : ILogSink
    {
        private readonly object _fileLock = new();
        private readonly string _filePath;

        public FileLogSink(string filePath)
        {
            _filePath = filePath;

            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
        }

        public void Write(string loggerName, LogLevel level, string message, Exception exception)
        {
            StringBuilder line = new StringBuilder()
                .Append(DateTime.Now.ToString(@"yyyy-MM-dd HH:mm:ss.fff"))
                .Append(" [").Append(level).Append("] ")
                .Append(loggerName).Append(" - ").Append(message);

            if (exception != null)
            {
                line.Append(Environment.NewLine).Append(exception);
            }

            lock (_fileLock)
            {
                try
                {
                    File.AppendAllText(_filePath, line.ToString() + Environment.NewLine);
                }
                catch
                {
                    // Nothing else to log this failure to - swallow so a logging problem
                    // never becomes the cause of an application crash.
                }
            }
        }
    }
}

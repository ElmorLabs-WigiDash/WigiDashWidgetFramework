using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WigiDashWidgetFramework
{
    public enum LogLevel
    {
        TRACE,
        DEBUG,
        INFO,
        WARN,
        ERROR,
        FATAL
    }

    public class LogModel
    {
        public DateTime Timestamp { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Caller { get; set; }
        public string LogMessage { get; set; }
        public string VerboseLogMessage { get; set; }

        public LogModel(LogLevel level, string message, string verboseMessage = "", [CallerMemberName] string caller = "")
        {
            LogLevel = level;
            LogMessage = message;
            VerboseLogMessage = verboseMessage;

            Caller = string.IsNullOrEmpty(caller) ? LogUtils.GetCallingClassName() : caller;

            Timestamp = DateTime.Now;
        }
    }

    public class LogUtils
    {
        public static string GetCallingClassName()
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var callingMethod = stackTrace.GetFrame(2).GetMethod();
            return callingMethod.ReflectedType?.Name ?? "Unknown";
        }
    }

    public static class Logger
    {
        // Queue for logs
        private static readonly Queue<LogModel> LogQueue = new Queue<LogModel>();

        // Write logFilePath for logs
        private static string _logPath;

        // Logger running flag
        private static bool _running;

        // Mutex to lock log writing
        private static readonly Mutex LogMutex = new Mutex();

        // Enabled log levels
        public static LogLevel[] EnabledLogLevels = { LogLevel.TRACE, LogLevel.DEBUG, LogLevel.INFO, LogLevel.WARN, LogLevel.ERROR, LogLevel.FATAL };

        // Log level control methods

        // Enable log level
        public static void EnableLogLevel(LogLevel level)
        {
            if (!EnabledLogLevels.Contains(level))
            {
                EnabledLogLevels = EnabledLogLevels.Append(level).ToArray();
            }
        }

        // Disable log level
        public static void DisableLogLevel(LogLevel level)
        {
            if (EnabledLogLevels.Contains(level))
            {
                EnabledLogLevels = EnabledLogLevels.Where(l => l != level).ToArray();
            }
        }

        // Disable all log levels
        public static void DisableAllLogLevels()
        {
            EnabledLogLevels = Array.Empty<LogLevel>();
        }

        // Enable all log levels
        public static void EnableAllLogLevels()
        {
            EnabledLogLevels = new[] { LogLevel.TRACE, LogLevel.DEBUG, LogLevel.INFO, LogLevel.WARN, LogLevel.ERROR, LogLevel.FATAL };
        }

        // Set minimum and maximum log levels
        public static void SetLogLevel(LogLevel minLevel, LogLevel maxLevel)
        {
            EnabledLogLevels = Array.Empty<LogLevel>();

            for (var i = (int)minLevel; i <= (int)maxLevel; i++)
            {
                EnabledLogLevels = EnabledLogLevels.Append((LogLevel)i).ToArray();
            }
        }

        // Log methods
        public static void Trace(string message, string verboseMessage = "")
        {
            Log(LogLevel.TRACE, message, verboseMessage);
        }

        public static void Trace(Exception ex, string message = "")
        {
            string logMessage = string.IsNullOrEmpty(message) ? ex.Message : message;

            Log(LogLevel.TRACE, logMessage, ex.ToString());
        }

        public static void Debug(string message, string verboseMessage = "")
        {
            Log(LogLevel.DEBUG, message, verboseMessage);
        }

        public static void Debug(Exception ex, string message = "")
        {
            string logMessage = string.IsNullOrEmpty(message) ? ex.Message : message;

            Log(LogLevel.DEBUG, logMessage, ex.ToString());
        }

        public static void Info(string message, string verboseMessage = "")
        {
            Log(LogLevel.INFO, message, verboseMessage);
        }

        public static void Info(Exception ex, string message = "")
        {
            string logMessage = string.IsNullOrEmpty(message) ? ex.Message : message;

            Log(LogLevel.INFO, logMessage, ex.ToString());
        }

        public static void Warn(string message, string verboseMessage = "")
        {
            Log(LogLevel.WARN, message, verboseMessage);
        }

        public static void Warn(Exception ex, string message = "")
        {
            string logMessage = string.IsNullOrEmpty(message) ? ex.Message : message;

            Log(LogLevel.WARN, logMessage, ex.ToString());
        }

        public static void Error(string message, string verboseMessage = "")
        {
            Log(LogLevel.ERROR, message, verboseMessage);
        }

        public static void Error(Exception ex, string message = "")
        {
            string logMessage = string.IsNullOrEmpty(message) ? ex.Message : message;

            Log(LogLevel.ERROR, logMessage, ex.ToString());
        }

        public static void Fatal(string message, string verboseMessage = "")
        {
            Log(LogLevel.FATAL, message, verboseMessage);
        }

        public static void Fatal(Exception ex, string message = "")
        {
            string logMessage = string.IsNullOrEmpty(message) ? ex.Message : message;

            Log(LogLevel.FATAL, logMessage, ex.ToString());
        }

        public static void Log(LogLevel level, Exception ex, string message = "")
        {
            string logMessage = string.IsNullOrEmpty(message) ? ex.Message : message;

            Log(level, logMessage, ex.ToString());
        }

        public static void Log(LogLevel level, string message, string verboseMessage = "")
        {
            LogQueue.Enqueue(new LogModel(level, message, verboseMessage));
        }

        public static void Log(LogModel log)
        {
            LogQueue.Enqueue(log);
        }

        // Initialize method to set file logFilePath and start logging
        public static void Init(string logFilePath)
        {
            _logPath = logFilePath;
            _running = true;

            Task.Run(LogTask);
        }

        // Stop logging
        public static void Stop()
        {
            _running = false;
        }

        // Log loop
        private static async Task LogTask()
        {
            while (_running)
            {
                if (LogQueue.Count > 0)
                {
                    var log = LogQueue.Dequeue();
                    await WriteLog(log);
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }

        // Write log to file
        private static async Task WriteLog(LogModel log)
        {
            // Check if log level is enabled
            if (!EnabledLogLevels.Contains(log.LogLevel))
            {
                return;
            }

            LogMutex.WaitOne();
            try
            {
                using var writer = new System.IO.StreamWriter(_logPath, true);

                await writer.WriteLineAsync($"{log.Timestamp} [{log.LogLevel}] {log.Caller}: {log.LogMessage}");
                if (!string.IsNullOrEmpty(log.VerboseLogMessage))
                {
                    await writer.WriteLineAsync(log.VerboseLogMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing log: {ex.Message}");
            }
            finally
            {
                LogMutex.ReleaseMutex();
            }
        }
    }
}

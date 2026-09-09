using System;

namespace WigiDashWidgetFramework.Logging
{
    // Per-class log handle, obtained via LogManager.GetCurrentClassLogger()/GetLogger().
    // Mirrors the shape of NLog's Logger so existing call sites (Logger.Error(ex, "..."),
    // Logger.Log(level, "..."), etc.) keep working unchanged against this abstraction.
    public sealed class Logger
    {
        public string Name { get; }

        internal Logger(string name)
        {
            Name = name;
        }

        public void Log(LogLevel level, string message) => LogManager.Write(Name, level, message, null);
        public void Log(LogLevel level, Exception exception, string message) => LogManager.Write(Name, level, message, exception);

        public void Trace(string message) => Log(LogLevel.TRACE, message);

        public void Debug(string message) => Log(LogLevel.DEBUG, message);

        public void Info(string message) => Log(LogLevel.INFO, message);
        public void Info(string message, params object[] args) => Log(LogLevel.INFO, Format(message, args));
        public void Info(Exception exception, string message) => Log(LogLevel.INFO, exception, message);

        public void Warn(string message) => Log(LogLevel.WARN, message);
        public void Warn(string message, params object[] args) => Log(LogLevel.WARN, Format(message, args));
        public void Warn(Exception exception, string message) => Log(LogLevel.WARN, exception, message);

        public void Error(string message) => Log(LogLevel.ERROR, message);
        public void Error(string message, params object[] args) => Log(LogLevel.ERROR, Format(message, args));
        public void Error(Exception exception, string message) => Log(LogLevel.ERROR, exception, message);

        public void Fatal(string message) => Log(LogLevel.FATAL, message);
        public void Fatal(Exception exception, string message) => Log(LogLevel.FATAL, exception, message);
        public void Fatal(object value) => Log(LogLevel.FATAL, value?.ToString());

        private static string Format(string message, object[] args)
        {
            if (args is not { Length: > 0 }) return message;

            try
            {
                return string.Format(message, args);
            }
            catch (FormatException)
            {
                // message wasn't actually a format template (e.g. a raw string passed where a
                // template was expected) - fall back to the literal message rather than
                // letting a logging call throw.
                return message;
            }
        }
    }
}

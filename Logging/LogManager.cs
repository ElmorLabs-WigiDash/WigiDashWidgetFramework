using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WigiDashWidgetFramework.Logging
{
    // Central logging entry point shared by the Manager process and every widget assembly
    // loaded into it. Nothing outside this file (and Logger/ILogSink/FileLogSink) needs to
    // know what actually writes the log out - swap the backend with SetSink(), including
    // to a wrapper around NLog, Serilog, or any other library, without touching call sites.
    public static class LogManager
    {
        private static readonly object Lock = new();
        private static ILogSink _sink;

        // Matches the previous NLog configuration (AddRule(Debug, Fatal, ...)): every level
        // except Trace is enabled by default.
        private static readonly HashSet<LogLevel> EnabledLevels = new(
            Enum.GetValues(typeof(LogLevel)).Cast<LogLevel>().Where(level => level != LogLevel.TRACE));

        public static void SetSink(ILogSink sink)
        {
            lock (Lock) _sink = sink;
        }

        public static Logger GetLogger(string name) => new(name);

        public static Logger GetCurrentClassLogger()
        {
            Type declaringType = new StackFrame(1, false).GetMethod()?.DeclaringType;
            return new Logger(declaringType?.FullName ?? "Unknown");
        }

        public static bool IsLevelEnabled(LogLevel level)
        {
            lock (Lock) return EnabledLevels.Contains(level);
        }

        public static void EnableLevel(LogLevel level)
        {
            lock (Lock) EnabledLevels.Add(level);
        }

        public static void DisableLevel(LogLevel level)
        {
            lock (Lock) EnabledLevels.Remove(level);
        }

        public static void DisableAllLevels()
        {
            lock (Lock) EnabledLevels.Clear();
        }

        public static List<LogLevel> GetEnabledLevels()
        {
            lock (Lock) return EnabledLevels.ToList();
        }

        internal static void Write(string loggerName, LogLevel level, string message, Exception exception)
        {
            if (!IsLevelEnabled(level)) return;

            ILogSink sink;
            lock (Lock) sink = _sink;

            sink?.Write(loggerName, level, message, exception);
        }
    }
}

using System;

namespace WigiDashWidgetFramework.Logging
{
    // Implement this to forward log output anywhere - a file, a UI panel, a third-party
    // library like NLog/Serilog, a remote endpoint, etc. - and register it with
    // LogManager.SetSink(). Only one sink is active at a time.
    public interface ILogSink
    {
        void Write(string loggerName, LogLevel level, string message, Exception exception);
    }
}

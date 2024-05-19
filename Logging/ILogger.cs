using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WigiDashWidgetFramework;

public interface ILogger
{
    public delegate void LogEventHandler(LogModel log);
    public event LogEventHandler? LogEvent;
    public LogLevel[] GetEnabledLogLevels();
    public void EnableLogLevel(LogLevel level);
    public void DisableLogLevel(LogLevel level);
    public void DisableAllLogLevels();
    public void EnableAllLogLevels();
    public void SetLogLevel(LogLevel minLevel, LogLevel maxLevel);
    public void Trace(string message, string verboseMessage = "");
    public void Trace(Exception ex, string message = "");
    public void Debug(string message, string verboseMessage = "");
    public void Debug(Exception ex, string message = "");
    public void Info(string message, string verboseMessage = "");
    public void Info(Exception ex, string message = "");
    public void Warn(string message, string verboseMessage = "");
    public void Warn(Exception ex, string message = "");
    public void Error(string message, string verboseMessage = "");
    public void Error(Exception ex, string message = "");
    public void Fatal(string message, string verboseMessage = "");
    public void Fatal(Exception ex, string message = "");
    public void Log(LogLevel level, Exception ex, string message = "");
    public void Log(LogLevel level, string message, string verboseMessage = "");
    public void Log(LogModel log);
    public void Init(string logFilePath);
    public void Stop();
}
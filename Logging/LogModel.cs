using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WigiDashWidgetFramework;

public class LogModel
{
    public DateTime Timestamp { get; set; }
    public LogLevel LogLevel { get; set; }
    public string Caller { get; set; }
    public string LogMessage { get; set; }
    public string VerboseLogMessage { get; set; }

    public LogModel(LogLevel level, string message, string verboseMessage = "", string caller = "")
    {
        LogLevel = level;
        LogMessage = message;
        VerboseLogMessage = verboseMessage;

        Caller = string.IsNullOrEmpty(caller) ? LogUtils.GetCallingClassName() : caller;

        Timestamp = DateTime.Now;
    }

    public override string ToString()
    {
        string timestampString = Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff");

        if (!string.IsNullOrEmpty(VerboseLogMessage))
        {
            return $"{timestampString} [{LogLevel}] {Caller}: {LogMessage}\n{VerboseLogMessage}";
        }
        else
        {
            return $"{timestampString} [{LogLevel}] {Caller}: {LogMessage}";
        }
    }

    public static LogModel FromString(string logString)
    {
        string[] parts = logString.Split('\n');

        // Parse header
        string[] headerParts = parts[0].Split(' ');

        string timestampString = headerParts[0] + " " + headerParts[1];
        string logLevelString = headerParts[2].Trim('[', ']');
        string caller = headerParts[3].Trim(':');

        DateTime timestamp = DateTime.ParseExact(timestampString, "yyyy-MM-dd HH:mm:ss.fff", null);
        LogLevel logLevel = (LogLevel)Enum.Parse(typeof(LogLevel), logLevelString);

        // Parse body
        string[] bodyParts = parts[1].Split('\n');

        string logMessage = bodyParts[0];
        string verboseLogMessage = bodyParts.Length > 2 ? bodyParts[1] : "";

        // Create log model
        return new LogModel(logLevel, logMessage, verboseLogMessage, caller)
        {
            Timestamp = timestamp
        };
    }
}
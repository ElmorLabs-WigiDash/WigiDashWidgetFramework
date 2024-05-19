using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WigiDashWidgetFramework;

public class LogUtils
{
    public static string GetCallingClassName()
    {
        var stackTrace = new System.Diagnostics.StackTrace();

        // Calling frame should be outside of LogUtils class,
        // Logger class,
        // IWidgetManager interface,
        // and LogModel class

        for (var i = 0; i < stackTrace.FrameCount; i++)
        {
            var frame = stackTrace.GetFrame(i);
            var method = frame.GetMethod();

            if (method.ReflectedType != typeof(LogUtils) &&
                method.ReflectedType != typeof(ILogger) &&
                method.ReflectedType != typeof(IWidgetManager) &&
                method.ReflectedType != typeof(LogModel))
            {
                // Return the namespace and class name
                return method.ReflectedType?.FullName ?? "Unknown";
            }
        }

        return "Unknown";
    }
}
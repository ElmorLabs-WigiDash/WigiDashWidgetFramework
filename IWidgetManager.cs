using System;

namespace FrontierWidgetFramework
{
    public class FullScreenEventArgs : EventArgs
    {
        public Guid InstanceGuid;
    }

    public delegate void FullScreenEnteredEventHandler(FullScreenEventArgs e);
    public delegate void FullScreenExitedEventHandler(FullScreenEventArgs e);

    public interface IWidgetManager
    {
        // Definition
        WidgetUtility.SdkVersion CurrentSdkVersion { get; }

        // Events
        event FullScreenEnteredEventHandler FullScreenEntered;
        event FullScreenExitedEventHandler FullScreenExited;

        // Functionality
        bool StoreSetting(IWidgetInstance instance, string name, string value);
        bool LoadSetting(IWidgetInstance instance, string name, out string value);
    }
}

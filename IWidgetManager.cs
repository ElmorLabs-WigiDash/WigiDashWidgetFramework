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
        bool StoreSetting(Guid instance_guid, string name, string value);
        bool LoadSetting(Guid instance_guid, string name, out string value);
    }
}

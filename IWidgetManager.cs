using System;
using System.Collections.Generic;

namespace FrontierWidgetFramework
{
    public class FullScreenEventArgs : EventArgs
    {
        public Guid InstanceGuid;
    }

    public delegate void FullScreenEnteredEventHandler(FullScreenEventArgs e);
    public delegate void FullScreenExitedEventHandler(FullScreenEventArgs e);
    public delegate void SensorUpdatedEventHandler(SensorItem item, double value);

    public interface IWidgetManager
    {
        // Definition
        WidgetUtility.SdkVersion CurrentSdkVersion { get; }

        // Events
        event FullScreenEnteredEventHandler FullScreenEntered;
        event FullScreenExitedEventHandler FullScreenExited;
        public event SensorUpdatedEventHandler SensorUpdated;

        // Functionality
        bool StoreSetting(IWidgetInstance instance, string name, string value);
        bool LoadSetting(IWidgetInstance instance, string name, out string value);

        // Hardware sensors
        bool AddMonitoringItem(SensorItem item);
        bool RemoveMonitoringItem(SensorItem item);
        List<SensorItem> GetSensorList();

    }

    public class SensorItem {
        
        public int ReadingType;
        public int SensorId1;
        public int SensorId2;
        public string Name;
        public string Unit;

        public SensorItem(int reading_type, int sensor_id1, int sensor_id2, string name, string unit) {
            SensorId1 = sensor_id1;
            SensorId2 = sensor_id2;
            ReadingType = reading_type;
            Name = name;
            Unit = unit;
        }
    }
}

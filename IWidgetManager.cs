using System;
using System.Collections.Generic;

namespace FrontierWidgetFramework
{

    public delegate void FullScreenEnteredEventHandler(Guid instance_guid);
    public delegate void FullScreenExitedEventHandler(Guid instance_guid);
    public delegate void SensorUpdatedEventHandler(SensorItem item, double value);
    public delegate void ActionRequestedEventHandler(Guid action_guid);

    public interface IWidgetManager
    {
        // Definition
        WidgetUtility.SdkVersion CurrentSdkVersion { get; }

        // Events
        event FullScreenEnteredEventHandler FullScreenEntered;
        event FullScreenExitedEventHandler FullScreenExited;
        event SensorUpdatedEventHandler SensorUpdated;
        event ActionRequestedEventHandler ActionRequested;

        // Functionality
        bool StoreSetting(IWidgetInstance widget_instance, string name, string value);
        bool LoadSetting(IWidgetInstance widget_instance, string name, out string value);

        // Hardware sensors
        bool AddMonitoringItem(SensorItem item);
        bool RemoveMonitoringItem(SensorItem item);
        List<SensorItem> GetSensorList();

        // Action Center Triggers
        Dictionary<Guid, string> GetTriggerList();
        bool RegisterTrigger(IWidgetInstance widget_instance, Guid trigger_guid, string trigger_name);
        bool UnregisterTrigger(IWidgetInstance widget_instance, Guid trigger_guid);
        void OnTriggerOccurred(Guid trigger_guid);

        // Action Center Actions
        Dictionary<Guid, string> GetActionList();
        bool RegisterAction(IWidgetInstance widget_instance, Guid action_guid, string name);
        bool UnregisterAction(IWidgetInstance widget_instance, Guid action_guid);
    }

    public class SensorItem {

        public Guid Guid;
        public int ReadingType;
        public int SensorId1;
        public int SensorId2;
        public string Source;
        public string Name;
        public string Unit;

        public SensorItem(Guid guid, int reading_type, int sensor_id1, int sensor_id2, string source, string name, string unit) {
            Guid = guid;
            SensorId1 = sensor_id1;
            SensorId2 = sensor_id2;
            ReadingType = reading_type;
            Source = source;
            Name = name;
            Unit = unit;
        }

    }
}

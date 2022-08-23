using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using FrontierWidgetFramework.WidgetUtility;

namespace FrontierWidgetFramework
{

    public delegate void FullScreenEnteredEventHandler(Guid page_guid, Guid instance_guid);
    public delegate void FullScreenExitedEventHandler(Guid page_guid, Guid instance_guid);
    public delegate void SensorUpdatedEventHandler(SensorItem item, double value);
    public delegate void ActionRequestedEventHandler(Guid action_guid);
    public delegate void GlobalThemeUpdateEventHandler();

    public interface IWidgetManager
    {
        // Definition
        WidgetUtility.SdkVersion CurrentSdkVersion { get; }

        // Theming
        WidgetTheme GlobalWidgetTheme { get; set; }

        // Events
        event FullScreenEnteredEventHandler FullScreenEntered;
        event FullScreenExitedEventHandler FullScreenExited;
        event SensorUpdatedEventHandler SensorUpdated;
        event ActionRequestedEventHandler ActionRequested;
        event GlobalThemeUpdateEventHandler GlobalThemeUpdated;

        // Functionality
        bool StoreSetting(IWidgetInstance widget_instance, string name, string value);
        bool LoadSetting(IWidgetInstance widget_instance, string name, out string value);

        bool RequestEnterFullScreen(IWidgetInstance widget_instance);
        bool RequestExitFullScreen(IWidgetInstance widget_instance);

        bool RequestPrivilegedExecution(IWidgetInstance widgetInstance, PrivilegeRequest request);

        Font RequestFontSelection(Font defaultFont);
        Color RequestColorSelection(Color defaultColor);

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
        void TriggerAction(Guid device_guid, Guid action_guid);
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

    public class WidgetTheme
    {
        public Font PrimaryFont { get; set; } = new Font("Basic Square 7", 32);
        public Font SecondaryFont { get; set; } = new Font("Basic Square 7", 26);
        public Color PrimaryFgColor { get; set; } = Color.White;
        public Color SecondaryFgColor { get; set; } = Color.Red;
        public Color PrimaryBgColor { get; set; } = Color.Black;
        public Color SecondaryBgColor { get; set; } = Color.Gray;
    }
}

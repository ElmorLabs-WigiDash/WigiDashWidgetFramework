using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using WigiDashWidgetFramework.WidgetUtility;

namespace WigiDashWidgetFramework
{

    public enum AidaStatus { Disconnected, Connected, Error };

    public enum LogLevel
    {
        TRACE,
        DEBUG,
        INFO,
        WARN,
        ERROR,
        FATAL
    }

    public delegate void FullScreenEnteredEventHandler(Guid pageGuid, Guid instanceGuid);
    public delegate void FullScreenExitedEventHandler(Guid pageGuid, Guid instanceGuid);
    public delegate void SensorUpdatedEventHandler(SensorItem item, double value);
    public delegate void ActionRequestedEventHandler(Guid actionGuid);
    public delegate void GlobalThemeUpdateEventHandler();
    public delegate void AidaWidgetUpdateEventHandler(Bitmap bitmap);
    public delegate void AidaStatusUpdatedEventHandler(AidaStatus status);

    public interface IWidgetManager
    {
        // Definition
        SdkVersion CurrentSdkVersion { get; }

        // Theming
        WidgetTheme GlobalWidgetTheme { get; set; }
        bool PreferGlobalTheme { get; set; }

        // Events
        event FullScreenEnteredEventHandler FullScreenEntered;
        event FullScreenExitedEventHandler FullScreenExited;
        event SensorUpdatedEventHandler SensorUpdated;
        event ActionRequestedEventHandler ActionRequested;
        event GlobalThemeUpdateEventHandler GlobalThemeUpdated;
        event AidaWidgetUpdateEventHandler AidaWidgetUpdated;
        event AidaStatusUpdatedEventHandler AidaStatusUpdated;

        // Functionality
        bool StoreSetting(IWidgetInstance widgetInstance, string settingId, string settingValue, bool isCloneable = true);
        bool LoadSetting(IWidgetInstance widgetInstance, string settingId, out string settingValue);

        bool StoreFile(IWidgetInstance widgetInstance, string fileId, string sourceFilePath, out string storedFilePath);
        bool LoadFile(IWidgetInstance widgetInstance, string fileId, out string storedFilePath);
        bool RemoveFile(IWidgetInstance widgetInstance, string fileId);
        bool CreateFile(IWidgetInstance widgetInstance, string fileId, string newFileName, out string storedFilePath);

        bool RequestEnterFullScreen(IWidgetInstance widgetInstance);
        bool RequestExitFullScreen(IWidgetInstance widgetInstance);

        //bool RequestPrivilegedExecution(IWidgetInstance widgetInstance, PrivilegeRequest request);

        Font RequestFontSelection(Font defaultFontSelection);
        Color RequestColorSelection(Color defaultColorSelection);
        string RequestImageSelection(string defaultImagePath);

        // Hardware sensors
        bool AddMonitoringItem(SensorItem sensorItem);
        bool RemoveMonitoringItem(SensorItem sensorItem);
        List<SensorItem> GetSensorList();

        // Action Center Triggers
        Dictionary<Guid, string> GetTriggerList();
        bool RegisterTrigger(IWidgetInstance widgetInstance, Guid triggerGuid, string triggerName);
        bool UnregisterTrigger(IWidgetInstance widgetInstance, Guid triggerGuid);
        void OnTriggerOccurred(Guid triggerGuid);

        // Action Center Actions
        string GetActionString(Guid deviceGuid, Guid actionGuid);
        
        bool CreateAction(Guid deviceGuid, Guid actionGuid, string actionName, out Guid actionGuidOut);
        bool EditAction(Guid deviceGuid, Guid actionGuid, string actionName = "");
        bool RemoveAction(Guid deviceGuid, Guid actionGuid);

        bool BindAction(IWidgetInstance widgetInstance, Guid actionGuid);
        bool UnbindAction(IWidgetInstance widgetInstance, Guid actionGuid);
        List<Guid> GetBoundActions(IWidgetInstance widgetInstance, int setId);

        bool RegisterAction(IWidgetInstance widgetInstance, Guid actionGuid, string actionName);
        bool UnregisterAction(IWidgetInstance widgetInstance, Guid actionGuid);
        void TriggerAction(Guid actionGuid);

        Guid? GetParentDevice(IWidgetInstance widgetInstance);

        // Aida
        bool RegisterAidaWidget(IWidgetInstance widgetInstance);
        bool UnregisterAidaWidget(IWidgetInstance widgetInstance);

        // Logging
        void WriteLogMessage(IWidgetInstance widgetInstance, LogLevel logLevel, string logMessage, string verboseLogMessage = "");
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
        public Font SecondaryFont { get; set; } = new Font("Basic Square 7 Solid", 26);
        public Color PrimaryFgColor { get; set; } = Color.White;
        public Color SecondaryFgColor { get; set; } = Color.Red;
        public Color PrimaryBgColor { get; set; } = Color.FromArgb(48, 48, 48);
        public Color SecondaryBgColor { get; set; } = Color.Gray;
        public int CornerRadius { get; set; } = 15;
    }
}

using FrontierWidgetFramework.WidgetUtility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;

namespace FrontierWidgetFramework
{
    public interface IWidgetInstance
    {
        // Definition
        public IWidgetObject WidgetObject { get; }
        public Guid Guid { get; }
        public WidgetSize WidgetSize { get; }
        public List<InstanceSetting> InstanceSettings { get; set; }

        // Events
        event WidgetUpdatedEventHandler WidgetUpdated;

        // Functionality
        public void RequestUpdate();
        public void ClickEvent(ClickType click_type, int x, int y);
        public UserControl GetSettingsControl();
        public void Dispose();
        public void EnterSleep();
        public void ExitSleep();
    }
    public class WidgetUpdatedEventArgs : EventArgs
    {
        public Point Offset { get; set; }
        public Bitmap WidgetBitmap { get; set; }
        public int WaitMax { get; set; }
    }

    public delegate void WidgetUpdatedEventHandler(IWidgetInstance widget_instance, WidgetUpdatedEventArgs e);
}

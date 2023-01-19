﻿using WigiDashWidgetFramework.WidgetUtility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;

namespace WigiDashWidgetFramework
{
    public interface IWidgetInstance : IDisposable
    {
        // Definition
        public IWidgetObject WidgetObject { get; }
        public Guid Guid { get; }
        public WidgetSize WidgetSize { get; }

        // Events
        event WidgetUpdatedEventHandler WidgetUpdated;

        // Functionality
        public void RequestUpdate();
        public void ClickEvent(ClickType click_type, int x, int y);
        public UserControl GetSettingsControl();
        public void EnterSleep();
        public void ExitSleep();
    }

    public interface IWidgetInstanceWithRemoval : IWidgetInstance
    {
        public void OnRemove();
    }

    public class WidgetUpdatedEventArgs : EventArgs
    {
        private readonly object BitmapLock = new();
        private Bitmap _currentBitmap;

        public Point Offset { get; set; }

        public Bitmap WidgetBitmap
        {
            get
            {
                lock (BitmapLock)
                {
                    return new Bitmap(_currentBitmap);
                }
            }
            set
            {
                Bitmap oldBitmap;
                Bitmap newBitmap = new Bitmap(value);

                lock (BitmapLock)
                {
                    oldBitmap = _currentBitmap;
                    _currentBitmap = newBitmap;
                }

                if (oldBitmap != null) oldBitmap.Dispose();
            }
        }

        public int WaitMax { get; set; }
    }

    public delegate void WidgetUpdatedEventHandler(IWidgetInstance widget_instance, WidgetUpdatedEventArgs e);
}

using FrontierWidgetFramework.WidgetUtility;
using System;
using System.Drawing;

namespace FrontierWidgetFramework
{
    public interface IWidgetObject : IWidgetBase
    {

        // Function
        IWidgetManager WidgetManager { get; set; }
        public Bitmap GetWidgetPreview(WidgetSize widgetSize);
        public IWidgetInstance CreateWidgetInstance(WidgetSize widgetSize, Guid instanceGuid);
        public bool RemoveWidgetInstance(Guid instanceGuid);

        // Error Handling
        WidgetError Load(string ResourcePath);
        WidgetError Unload();
        public string LastErrorMessage { get; set; }

    }
}

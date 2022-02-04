using FrontierWidgetFramework.WidgetUtility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontierWidgetFramework
{
    public interface IWidgetObject
    {
        // Definition
        public Guid Guid { get; }
        public string Name { get; }
        public string Description { get; }
        public string Author { get; }
        public string Website { get; }
        public Version Version { get; }
        public List<WidgetSize> SupportedSizes { get; }

        // Function
        IWidgetManager WidgetManager { get; set; }
        public Bitmap GetWidgetPreview(WidgetSize widgetSize);
        public IWidgetInstance CreateWidgetInstance(WidgetSize widgetSize, Guid instanceGuid);
        public bool RemoveWidgetInstance(Guid instanceGuid);

        // Error Handling
        WidgetError Load(string ResourcePath);
        WidgetError Unload();
        public string LastErrorMessage { get; set;  }
    }
}

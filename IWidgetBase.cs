using FrontierWidgetFramework.WidgetUtility;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FrontierWidgetFramework
{
    public interface IWidgetBase
    {
        // Definition
        public Guid Guid { get; }
        public string Name { get; }
        public string Author { get; }
        public string Website { get; }
        public string Description { get;  }
        public Version Version { get; }
        public SdkVersion TargetSdk { get; }
        public List<WidgetSize> SupportedSizes { get; }
        public Bitmap PreviewImage { get; }
    }
}

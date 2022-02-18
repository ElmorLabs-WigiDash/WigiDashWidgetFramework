using FrontierWidgetFramework.WidgetUtility;
using System;

namespace FrontierWidgetFramework
{
    public interface IWidgetBase
    {
        // Definition
        public Guid Guid { get; }
        public string Name { get; }
        public string Author { get; }
        public string Website { get; }
        public Version Version { get; }
        public SdkVersion TargetSdk { get; }
    }
}

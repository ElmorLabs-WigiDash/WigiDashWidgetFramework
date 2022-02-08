using FrontierWidgetFramework.WidgetUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

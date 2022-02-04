using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontierWidgetFramework.Utility
{
    public static class HelperUtility
    {
        public static Color ColorTo16bit(Color color)
        {

            int red = (color.R * 31 / 255) * 255 / 31;
            int green = (color.G * 63 / 255) * 255 / 63;
            int blue = (color.B * 31 / 255) * 255 / 31;

            return Color.FromArgb(red, green, blue);
        }
        public static Color ColorFromRgb565(UInt16 clr16bit)
        {
            int r = (clr16bit >> 11) << 3; // 5-bit
            int g = ((clr16bit >> 5) & 0x3F) << 2; // 6-bit
            int b = (clr16bit & 0x1F) << 3; // 5-bit
            return Color.FromArgb(r, g, b);
        }

        public static UInt16 ColorToRgb565(Color color)
        {
            return (UInt16)(((color.R >> 3) << 11) | ((color.G >> 2) << 5) | ((color.B >> 3)));
        }
    }
}

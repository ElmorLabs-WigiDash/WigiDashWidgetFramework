using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontierWidgetFramework.WidgetUtility
{
    public enum ClickType : int
    {
        None, Single, Double, Long
    }

    public struct ClickInfo
    {
        public ClickType Type;
        public int Id;
        public int X;
        public int Y;
        public int Duration;
    }

    public enum WidgetError : int
    {
        // Clear
        NO_ERROR,

        // SDK Version Errors
        SDK_VERSION_NOT_SUPPORTED,

        // Manager Error
        MANAGER_LOAD_FAIL,

        // Others
        UNDEFINED_ERROR,
        CUSTOM_ERROR
    }

    public enum SdkVersion : int
    {
        Version_0, // ALPHA
    }

    // Widget Size & Position Classes
    public class WidgetBaseUnit
    {
        public const int Width = 204;
        public const int Height = 149;
        public const int OffsetX = 4;
        public const int OffsetY = 4;
    }

    public class WidgetSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public WidgetSize() { }
        public WidgetSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return Width.ToString() + "x" + Height.ToString();
        }

        public Size ToSize()
        {
            return new Size(Width * WidgetBaseUnit.Width - WidgetBaseUnit.OffsetX, Height * WidgetBaseUnit.Height - WidgetBaseUnit.OffsetY);
        }
    }

    // Utility Functions
    public static class WidgetUtility
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

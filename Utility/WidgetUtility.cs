using FrontierWidgetFramework.GridUtility;
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

    public enum WidgetSize : int
    {
        SIZE_1X1, SIZE_1X2, SIZE_1X3, SIZE_1X4, SIZE_2X1, SIZE_2X2, SIZE_2X3, SIZE_2X4, SIZE_3X1, SIZE_3X2, SIZE_3X3, SIZE_3X4, SIZE_4X1, SIZE_4X2, SIZE_4X3, SIZE_4X4, SIZE_5X1, SIZE_5X2, SIZE_5X3, SIZE_5X4
    }

    public enum WidgetError : int
    {
        // Clear
        NO_ERROR,

        // SDK Version Errors
        SDK_VERSION_TOO_HIGH,
        SDK_VERSION_TOO_LOW,
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

    public static class Extensions
    {
        private const int base_width = 200;
        private const int base_height = 145;
        private const int offset_x = 4;
        private const int offset_y = 4;

        public static Size ToSize(this WidgetSize widgetSize)
        {
            switch (widgetSize)
            {
                case WidgetSize.SIZE_1X1:
                    return new Size(base_width, base_height);
                case WidgetSize.SIZE_1X2:
                    return new Size(base_width, base_height * 2 + offset_y);
                case WidgetSize.SIZE_1X3:
                    return new Size(base_width, base_height * 3 + offset_y * 2);
                case WidgetSize.SIZE_1X4:
                    return new Size(base_width, base_height * 4 + offset_y * 3);
                case WidgetSize.SIZE_2X1:
                    return new Size(base_width * 2 + offset_x, base_height);
                case WidgetSize.SIZE_2X2:
                    return new Size(base_width * 2 + offset_x, base_height * 2 + offset_y);
                case WidgetSize.SIZE_2X3:
                    return new Size(base_width * 2 + offset_x, base_height * 3 + offset_y * 2);
                case WidgetSize.SIZE_2X4:
                    return new Size(base_width * 2 + offset_x, base_height * 4 + offset_y * 3);
                case WidgetSize.SIZE_3X1:
                    return new Size(base_width * 3 + offset_x * 2, base_height);
                case WidgetSize.SIZE_3X2:
                    return new Size(base_width * 3 + offset_x * 2, base_height * 2 + offset_y);
                case WidgetSize.SIZE_3X3:
                    return new Size(base_width * 3 + offset_x * 2, base_height * 3 + offset_y * 2);
                case WidgetSize.SIZE_3X4:
                    return new Size(base_width * 3 + offset_x * 2, base_height * 4 + offset_y * 3);
                case WidgetSize.SIZE_4X1:
                    return new Size(base_width * 4 + offset_x * 3, base_height);
                case WidgetSize.SIZE_4X2:
                    return new Size(base_width * 4 + offset_x * 3, base_height * 2 + offset_y);
                case WidgetSize.SIZE_4X3:
                    return new Size(base_width * 4 + offset_x * 3, base_height * 3 + offset_y * 2);
                case WidgetSize.SIZE_4X4:
                    return new Size(base_width * 4 + offset_x * 3, base_height * 4 + offset_y * 3);
                case WidgetSize.SIZE_5X1:
                    return new Size(base_width * 5 + offset_x * 4, base_height);
                case WidgetSize.SIZE_5X2:
                    return new Size(base_width * 5 + offset_x * 4, base_height * 2 + offset_y);
                case WidgetSize.SIZE_5X3:
                    return new Size(base_width * 5 + offset_x * 4, base_height * 3 + offset_y * 2);
                case WidgetSize.SIZE_5X4:
                    return new Size(base_width * 5 + offset_x * 4, base_height * 4 + offset_y * 3);
            }
            throw new NotImplementedException();
        }

        public static GridSize ToGridSize(this WidgetSize widgetSize)
        {
            switch (widgetSize)
            {
                case WidgetSize.SIZE_1X1:
                    return new GridSize(1, 1);
                case WidgetSize.SIZE_1X2:
                    return new GridSize(1, 2);
                case WidgetSize.SIZE_1X3:
                    return new GridSize(1, 3);
                case WidgetSize.SIZE_1X4:
                    return new GridSize(1, 4);
                case WidgetSize.SIZE_2X1:
                    return new GridSize(2, 1);
                case WidgetSize.SIZE_2X2:
                    return new GridSize(2, 2);
                case WidgetSize.SIZE_2X3:
                    return new GridSize(2, 3);
                case WidgetSize.SIZE_2X4:
                    return new GridSize(2, 4);
                case WidgetSize.SIZE_3X1:
                    return new GridSize(3, 1);
                case WidgetSize.SIZE_3X2:
                    return new GridSize(3, 2);
                case WidgetSize.SIZE_3X3:
                    return new GridSize(3, 3);
                case WidgetSize.SIZE_3X4:
                    return new GridSize(3, 4);
                case WidgetSize.SIZE_4X1:
                    return new GridSize(4, 1);
                case WidgetSize.SIZE_4X2:
                    return new GridSize(4, 2);
                case WidgetSize.SIZE_4X3:
                    return new GridSize(4, 3);
                case WidgetSize.SIZE_4X4:
                    return new GridSize(4, 4);
                case WidgetSize.SIZE_5X1:
                    return new GridSize(5, 1);
                case WidgetSize.SIZE_5X2:
                    return new GridSize(5, 2);
                case WidgetSize.SIZE_5X3:
                    return new GridSize(5, 3);
                case WidgetSize.SIZE_5X4:
                    return new GridSize(5, 4);
            }
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontierWidgetFramework.GridUtility
{
    public class GridBaseUnit
    {
        public const int Width = 200;
        public const int Height = 145;
        public const int OffsetX = 4;
        public const int OffsetY = 4;
    }

    public class GridSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public GridSize() { }
        public GridSize(int width, int height)
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
            return new Size(Width * GridBaseUnit.Width, Height * GridBaseUnit.Height);
        }
    }

    public class GridPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public GridPosition() { }
        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return X.ToString() + "x" + Y.ToString();
        }
    }
}

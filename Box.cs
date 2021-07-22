
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Futura2
{
    public class Box
    {
        public int X1;
        public int Y1;
        public int X2;
        public int Y2;
        public Color Color;

        public Box(int _x, int _y, int _width, int _height)
        {
            X1 = _x;
            Y1 = _y;
            X2 = _x + _width;
            Y2 = _y + _height;
        }

        public Box(int _x, int _y, int _width, int _height, Color _color)
        {
            X1 = _x;
            Y1 = _y;
            X2 = _x + _width;
            Y2 = _y + _height;
            Color = _color;
        }
    }
}

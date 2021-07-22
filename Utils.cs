using Microsoft.Xna.Framework;

namespace Futura2
{
    public class Utils
    {
        public bool MouseInBox(Box box, Vector2 mousePos)
        {
            if (mousePos.X >= box.X1 && mousePos.X <= box.X2)
                if (mousePos.Y >= box.Y1 && mousePos.Y <= box.Y2)
                    return true;
            return false;
        }
    }
}

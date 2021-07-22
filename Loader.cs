using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Futura2
{
    public class Loader
    {
        public List<Box> LoadElements(GraphicsDeviceManager graphics, int elementCount)
        {
            List<Box> elements = new List<Box>();
            Random rnd = new Random();
            int boxWidth = 50;

            for (int i = 0; i < elementCount; i++)
            {
                elements.Add(new Box(rnd.Next(0, graphics.PreferredBackBufferWidth - boxWidth), rnd.Next(0, graphics.PreferredBackBufferHeight - boxWidth), boxWidth, 2 * boxWidth, new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255))));
            }
            return elements;
        }
    }
}

using Libraries;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNetworkGame.Logic
{
    public class Box : Drawable
    {
        public Point position;
        private Size size;

        public Box()
        {

            position = new Point(0, 0);
            size = new Size(80, 80);
        }

        public void OnUpdate()
        {
        }

        public void OnDraw(Graphics g)
        {
            g.TranslateTransform(position.X, position.Y);
            g.FillRectangle(Brushes.Blue, -size.Width / 2, -size.Height / 2, size.Width, size.Height);
            g.ResetTransform();
        }

        public Rectangle GetBound()
        {
            int x = position.X - (size.Width / 2);
            int y = position.Y - (size.Height / 2);

            return new Rectangle(x, y, size.Width, size.Height);
        }
    }
}

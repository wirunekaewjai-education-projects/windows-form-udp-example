using Libraries;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoomNetworkGame.Logic
{
    public class Player : User, Drawable
    {
        public int id;
        public Point position;
        private Size size;

        private SizeF measure;
        private bool isMeasured;

        public Player(IPEndPoint ep, string name, int id) : base(ep, name)
        {
            this.id = id;

            position = new Point(100, 100);
            size = new Size(30, 30);
            measure = new SizeF();
            isMeasured = false;
        }

        public void OnUpdate()
        {

        }

        public void OnDraw(Graphics g)
        {
            if(!isMeasured && !string.IsNullOrEmpty(name))
            {
                isMeasured = true;
                measure = g.MeasureString(name, Game.GetFont());
            }

            g.TranslateTransform(position.X, position.Y);
            g.FillEllipse(Brushes.Red, -size.Width / 2, -size.Height / 2, size.Width, size.Height);

            float px = - (measure.Width / 2);
            g.DrawString(name, Game.GetFont(), Brushes.Black, px, (size.Height / 2) + 5);
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

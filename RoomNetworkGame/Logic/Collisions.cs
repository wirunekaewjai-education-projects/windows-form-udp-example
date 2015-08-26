using Libraries;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNetworkGame.Logic
{
    public class Collisions
    {
        public static Point Collide(Drawable a, Drawable b, int dx, int dy)
        {
            Rectangle bbt = b.GetBound();

            Rectangle ab = a.GetBound();
            Rectangle bb = b.GetBound();

            bb.X += dx;
            bb.Y += dy;

            Rectangle cc = Rectangle.Intersect(ab, bb);

            if (cc.IsEmpty)
            {
                //Rectangle un = Rectangle.Union(bb, bbt);
                //Rectangle uc = Rectangle.Intersect(ab, un);

                //if (uc.IsEmpty)
                //    return Point.Empty;

                //// Incompleted
                return Point.Empty;

                //Rectangle abb = a.GetBound();
                //abb.X += -dx;
                //abb.Y += -dy;

                //Point gp = GetPoint(bb, ab, abb, Rectangle.Intersect(abb, bb), -dx, -dy);
                //return new Point(gp.X, gp.Y);
            }
            else
            {
                return GetPoint(ab, bb, bbt, cc, dx, dy);
            }
        }

        private static Point GetPoint(Rectangle ab, Rectangle bb, Rectangle bbt, Rectangle cc, int dx, int dy)
        {
            int fx = 0;
            int fy = 0;

            bool b1 = bb.Bottom >= ab.Top;
            bool b2 = bb.Top < ab.Bottom;

            bool b3 = bb.Right >= ab.Left && bb.Right < ab.Right;
            bool b4 = bb.Left >= ab.Left && bb.Left < ab.Right;

            bool b5 = bb.Bottom < ab.Bottom;
            bool b6 = bb.Top >= ab.Top;

            bool b7 = bbt.Top > bb.Top;
            bool b8 = bbt.Bottom < bb.Bottom;

            bool b9 = bbt.Left < bb.Left;
            bool b10 = bbt.Right > bb.Right;

            if (cc.Width < cc.Height)
            {
                bool b13 = b1 && b5;
                bool b14 = b2 && b6;

                if (b3 || (b13 && b14 && b9))
                    fx -= bb.Right - ab.Left;
                else if (b4 || (b13 && b14 && b10))
                    fx += ab.Right - bb.Left;
                else if (b13 && !b14)
                    fy -= bb.Bottom - ab.Top;
                else if (b14 && !b13)
                    fy += ab.Bottom - bb.Top;
            }
            else
            {
                if ((b1 && b5) || (b3 && b4 && b8))
                    fy -= bb.Bottom - ab.Top;
                else if ((b2 && b6) || (b3 && b4 && b7))
                    fy += ab.Bottom - bb.Top;
                else if (b3 && !b4)
                    fx -= bb.Right - ab.Left;
                else if (b4 && !b3)
                    fx += ab.Right - bb.Left;
            }

            return new Point(fx, fy);
        }

        //private static bool InBound(float ax, float ay, float bx, float by)
        //{

        //}

        public static float Dot(float ax, float ay, float bx, float by)
        {
            return ((ax * bx) + (ay * by));
        }

        public static float Cross(float ax, float ay, float bx, float by)
        {
            //PointF result = new PointF();

            //result.X = (ay * bz) - (az * by);
            //result.Y = (az * bx) - (ax * bz);
            return (ax * by) - (ay * bx);

            //return result;
        }

        private static PointF Magitude(float ax, float ay, float bx, float by)
        {
            float x = bx - ax;
            float y = by - ay;

            float length = (float)Math.Sqrt((x*x) + (y*y));

            return new PointF(x / length, y / length);
        }
    }
}

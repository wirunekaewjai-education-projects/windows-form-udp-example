using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries
{
    public interface Drawable
    {
        Rectangle GetBound();
        void OnUpdate();
        void OnDraw(System.Drawing.Graphics g);
    }
}

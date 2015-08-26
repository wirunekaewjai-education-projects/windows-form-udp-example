using Libraries.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libraries
{
    public class Invalidater
    {

        public static readonly int DRAW_RATE = 1000 / 30; // 30 Frame Per Seconds.
        public static readonly int UPDATE_RATE = 1000 / 60; // 60 Frame Per Seconds.

        private Timer mTimer;
        private List<Control> mControls;
        private List<IUpdate> mUpdates;

        private long lastTime;

        private Invalidater()
        {
            mControls = new List<Control>();
            mUpdates = new List<IUpdate>();

            lastTime = 0;

            mTimer = new Timer();
            mTimer.Interval = UPDATE_RATE;
            mTimer.Tick += OnTimerTick;
        }

        protected void OnTimerTick(object sender, EventArgs e)
        {
            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (now - lastTime >= DRAW_RATE)
            {
                foreach (Control control in mControls)
                {
                    control.Invalidate();
                }

                lastTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            }

            foreach (IUpdate update in mUpdates)
            {
                update.OnUpdate();
            }
        }

        public void Register(Control control)
        {
            if (mControls.Count + mControls.Count == 0)
                mTimer.Start();

            mControls.Add(control);
        }

        public void Unregister(Control control)
        {
            mControls.Remove(control);

            if (mControls.Count + mControls.Count == 0)
                mTimer.Stop();
        }

        public void Register(IUpdate update)
        {
            if (mUpdates.Count + mControls.Count == 0)
                mTimer.Start();

            mUpdates.Add(update);
        }

        public void Unregister(IUpdate update)
        {
            mUpdates.Remove(update);

            if (mUpdates.Count + mControls.Count == 0)
                mTimer.Stop();
        }

        private static Invalidater instance = null;
        public static Invalidater Instance
        {
            get
            {
                if (instance == null)
                    instance = new Invalidater();

                return instance;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libraries
{
    public partial class Scene : UserControl
    {
        private List<Drawable>[] mLayers;
        private Dictionary<Keys, bool> mInputs;

        public Scene()
        {
            InitializeComponent();
            DoubleBuffered = true;

            mLayers = new List<Drawable>[16];
            mInputs = new Dictionary<Keys, bool>();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Size = new System.Drawing.Size(Game.WINDOW_WIDTH, Game.WINDOW_HEIGHT);

            bool IsInDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime;

            if(!IsInDesignMode)
                Invalidater.Instance.Register(this);
        }

        public virtual void OnDestroy()
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            bool IsInDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime;

            if (!IsInDesignMode)
            {
                OnUpdate();
                OnDraw(e.Graphics);
            }

        }


        public void AddDrawable(int layer, Drawable d)
        {
            if (mLayers[layer] == null)
                mLayers[layer] = new List<Drawable>();

            mLayers[layer].Add(d);
        }

        public void RemoveDrawable(int layer, Drawable d)
        {
            if (mLayers[layer] == null)
                return;

            mLayers[layer].Remove(d);
        }


        protected override bool IsInputKey(Keys keyData)
        {
            return base.IsInputKey(keyData);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            mInputs[e.KeyCode] = true;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            mInputs[e.KeyCode] = false;
        }

        public bool IsPressing(Keys key)
        {
            if (mInputs.ContainsKey(key))
                return mInputs[key];

            return false;
        }

        protected virtual void OnUpdate() { }
        protected virtual void OnDraw(System.Drawing.Graphics g) 
        {
            //foreach (List<Drawable> layer in mLayers)
            //{
            //    foreach (Drawable drawable in layer)
            //    {
            //        drawable.OnDraw(g);
            //    }
            //}
        }
    }
}

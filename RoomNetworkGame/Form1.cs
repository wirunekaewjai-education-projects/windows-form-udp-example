using Libraries;
using RoomNetworkGame.Logic;
using RoomNetworkGame.Page;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomNetworkGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Size = new Size(Game.WINDOW_WIDTH, Game.WINDOW_HEIGHT);

            Game.SetForm(this);
            Game.SetScene(new MenuPage());
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Server.Stop();
            Game.Destroy();
        }
    }
}

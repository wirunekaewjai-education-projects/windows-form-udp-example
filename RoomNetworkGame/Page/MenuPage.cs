using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Libraries;
using RoomNetworkGame.Logic;

namespace RoomNetworkGame.Page
{
    public partial class MenuPage : Scene
    {

        public MenuPage()
        {
            InitializeComponent();
        }

        protected override void OnUpdate()
        {
            
        }

        protected override void OnDraw(Graphics g)
        {
            
        }

        private void OnHostClick(object sender, EventArgs e)
        {
            string hostName = hostNameTextBox.Text;
            if (string.IsNullOrEmpty(hostName))
            {
                MessageBox.Show("กรุณาใส่ชื่อ");
                return;
            }

            Server.Start();

            PlayingPage page = new PlayingPage();
            page.Initialize(hostName, "127.0.0.1");

            Game.SetScene(page);
        }

        private void OnJoinClick(object sender, EventArgs e)
        {
            string joinName = joinNameTextBox.Text;
            string hostIP = hostIPTextBox.Text;

            if (string.IsNullOrEmpty(joinName))
            {
                MessageBox.Show("กรุณาใส่ชื่อ");
                return;
            }

            if (string.IsNullOrEmpty(hostIP))
            {
                MessageBox.Show("กรุณาใส่ IP");
                return;
            }

            PlayingPage page = new PlayingPage();
            page.Initialize(joinName, hostIP);

            Game.SetScene(page);
        }
    }
}

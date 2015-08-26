using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RoomNetworkGame.Logic;
using Libraries;

namespace RoomNetworkGame.Page
{
    public partial class PlayingPage : Scene
    {
        private Client client;
        private string name;

        public PlayingPage()
        {
            InitializeComponent();
        }

        public void Initialize(string name, string hostIP)
        {
            this.name = name;
            this.client = new Client(name, hostIP);
            this.client.Connect();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            this.client.Disconnect();
        }

        protected override void OnUpdate()
        {
            if (null != client && client.IsDisconnected())
            {
                Game.SetScene(new MenuPage());
            }
        }

        protected override void OnDraw(Graphics g)
        {
            if (null != client)
            {
                foreach (Box box in client.boxes)
                {
                    box.OnDraw(g);
                }

                foreach (Player player in client.players.Values)
                {
                    player.OnDraw(g);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (null != client)
                client.SetKey(e.KeyCode, true);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (null != client)
                client.SetKey(e.KeyCode, false);
        }
    }
}

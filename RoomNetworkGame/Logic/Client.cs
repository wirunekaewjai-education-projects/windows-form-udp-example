using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomNetworkGame.Logic
{
    public class Client
    {
        private static readonly int CONNECT_PORT = 9111;

        private Udp udp;
        private string name;
        private string hostIP;
        private int id;

        private bool isDisconnected;

        public Dictionary<int, Player> players;
        public List<Box> boxes;

        public Client(string name, string hostIP)
        {
            this.name = name;
            this.hostIP = hostIP;

            this.players = new Dictionary<int, Player>();
            this.boxes = new List<Box>();

            this.udp = new Udp(CONNECT_PORT, OnReceive);
            this.udp.SetDestination(hostIP, Server.SERVER_PORT);

            this.isDisconnected = false;
        }

        public void OnReceive(IPEndPoint endpoint, string msg)
        {
            JObject json = JObject.Parse(msg);

            int cmd = (int)json[Tag.COMMAND];
            if (cmd == Cmd.CONNECT && (int)json[Tag.STATUS] == Status.SUCCESS)
            {
                int gamePort = (int)json[Tag.PORT];
                id = (int)json[Tag.ID];

                this.udp.Close();
                this.udp = new Udp(gamePort, OnReceive);
                this.udp.SetDestination(hostIP, Server.SERVER_PORT);

                JArray jps = json[Tag.PLAYERS] as JArray;
                if (null != jps)
                {
                    foreach (JObject jp in jps)
                    {
                        int _id = (int)jp[Tag.ID];
                        string ip = (string)jp[Tag.IP];
                        int port = (int)jp[Tag.PORT];
                        string name = (string)jp[Tag.NAME];
                        int tx = (int)jp[Tag.TX];
                        int ty = (int)jp[Tag.TY];

                        Player p = new Player(new IPEndPoint(IPAddress.Parse(ip), port), name, _id);
                        p.position = new System.Drawing.Point(tx, ty);

                        players[_id] = p;
                    }
                }

                JArray jbs = json[Tag.BOXES] as JArray;
                if (null != jbs)
                {
                    foreach (JObject jb in jbs)
                    {
                        int tx = (int)jb[Tag.TX];
                        int ty = (int)jb[Tag.TY];

                        Box box = new Box();
                        box.position = new System.Drawing.Point(tx, ty);
                        boxes.Add(box);
                    }
                }
            }
            else if (cmd == Cmd.ADD_PLAYER)
            {
                int _id = (int)json[Tag.ID];
                string ip = (string)json[Tag.IP];
                int port = (int)json[Tag.PORT];
                string name = (string)json[Tag.NAME];
                int tx = (int)json[Tag.TX];
                int ty = (int)json[Tag.TY];

                Player p = new Player(new IPEndPoint(IPAddress.Parse(ip), port), name, _id);
                p.position = new System.Drawing.Point(tx, ty);

                players[_id] = p;
            }
            else if (cmd == Cmd.DISCONNECT)
            {
                int _id = (int)json[Tag.ID];
                players.Remove(_id);
            }
            else if (cmd == Cmd.SERVER_CLOSED)
            {
                Disconnect();
            }
            else if (cmd == Cmd.UPDATE)
            {
                JArray jpos = json[Tag.POSITIONS] as JArray;
                foreach (JObject jp in jpos)
                {
                    int _id = (int)jp[Tag.ID];
                    if (players.ContainsKey(_id))
                    {
                        int tx = (int)jp[Tag.TX];
                        int ty = (int)jp[Tag.TY];

                        Player p = players[_id];
                        p.position.X = tx;
                        p.position.Y = ty;
                    }
                }
            }

        }


        public void Connect()
        {
            JObject json = new JObject();
            json[Tag.COMMAND] = Cmd.CONNECT;
            json[Tag.NAME] = name;

            udp.Send(json.ToString());
        }

        public void Disconnect()
        {
            if (!isDisconnected && null != udp)
            {
                isDisconnected = true;

                JObject json = new JObject();
                json[Tag.COMMAND] = Cmd.DISCONNECT;
                json[Tag.ID] = id;

                udp.Send(json.ToString());

                udp.Close();
                udp = null;
            }
        }

        public bool IsDisconnected()
        {
            return isDisconnected;
        }

        //
        public void SetKey(Keys key, bool down)
        {
            string keyText = "";
            switch (key)
            {
                case Keys.W: keyText = "UP"; break;
                case Keys.S: keyText = "DOWN"; break;
                case Keys.A: keyText = "LEFT"; break;
                case Keys.D: keyText = "RIGHT"; break;
                default : break;
            }

            if (!string.IsNullOrEmpty(keyText))
            {
                JObject json = new JObject();
                json[Tag.ID] = id;

                if (down)
                    json[Tag.COMMAND] = Cmd.KEY_DOWN;
                else
                    json[Tag.COMMAND] = Cmd.KEY_UP;

                json[Tag.KEY] = keyText;

                udp.Send(json.ToString());
            }
        }
    }
}

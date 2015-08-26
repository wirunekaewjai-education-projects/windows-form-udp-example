using Libraries;
using Libraries.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoomNetworkGame.Logic
{
    public class Server : IUpdate
    {
        public static int SERVER_PORT = 9999;

        private Udp udp;
        private int generatePort = 10000;
        private int generateId = 0;

        private Dictionary<int, Player> players;
        private Dictionary<int, Dictionary<string, bool>> keys;

        private List<Box> boxes;

        private Server()
        {
            players = new Dictionary<int, Player>();
            keys = new Dictionary<int, Dictionary<string, bool>>();
            boxes = new List<Box>();

            Box box1 = new Box();
            box1.position = new System.Drawing.Point(300, 100);
            Box box2 = new Box();
            box2.position = new System.Drawing.Point(200, 360);

            boxes.Add(box1);
            boxes.Add(box2);
        }

        public void OnUpdate()
        {
            if (players.Count == 0)
                return;

            JObject json = new JObject();
            json[Tag.COMMAND] = Cmd.UPDATE;

            JArray jpos = new JArray();
            foreach (Player player in players.Values)
            {
                int id = player.id;
                int dx = 0, dy = 0;

                if (isPressing(id, "UP"))
                {
                    dy -= 10;
                }
                else if (isPressing(id, "DOWN"))
                {
                    dy += 10;
                }

                if (isPressing(id, "LEFT"))
                {
                    dx -= 10;
                }
                else if (isPressing(id, "RIGHT"))
                {
                    dx += 10;
                }

                foreach (Box box in boxes)
                {
                    Point pc = Collisions.Collide(box, player, dx, dy);
                    dx += pc.X;
                    dy += pc.Y;
                }

                //foreach (Player p in players.Values)
                //{
                //    if (p != player)
                //    {
                //        Point pc = Collisions.Collide(p, player, dx, dy);
                //        dx += pc.X;
                //        dy += pc.Y;
                //    }
                //}

                player.position.X += dx;
                player.position.Y += dy;


                JObject jp = new JObject();
                jp[Tag.ID] = player.id;
                jp[Tag.TX] = player.position.X;
                jp[Tag.TY] = player.position.Y;

                jpos.Add(jp);
            }

            json[Tag.POSITIONS] = jpos;
            Broadcast(json.ToString());

        }

        private int GeneratePort(IPAddress addr)
        {
            string addrString = addr.ToString();
            foreach (Player player in players.Values)
            {
                if (player.endpoint.Address.ToString().Equals(addrString)
                    && player.endpoint.Port == generatePort)
                {
                    generatePort++;
                    Console.WriteLine("Gen Port : " + generatePort);
                    return generatePort;
                }
            }
            return generatePort;
        }

        //
        public void OnReceive(IPEndPoint clientEndPoint, string msg)
        {
            JObject json = JObject.Parse(msg);
            // TO DO

            int cmd = (int)json[Tag.COMMAND];
            if (cmd == Cmd.CONNECT)
            {
                string name = (string)json[Tag.NAME];
                int port = GeneratePort(clientEndPoint.Address);
                int id = generateId++;

                IPEndPoint nEndpoint = new IPEndPoint(clientEndPoint.Address, port);
                Player p = new Player(nEndpoint, name, id);
                keys[id] = new Dictionary<string, bool>();


                JObject res = new JObject();
                res[Tag.COMMAND] = Cmd.CONNECT;
                res[Tag.STATUS] = Status.SUCCESS;
                res[Tag.PORT] = port;
                res[Tag.ID] = id;

                JArray jbs = new JArray();
                res[Tag.BOXES] = jbs;
                foreach (Box box in boxes)
                {
                    JObject jb = new JObject();
                    jb[Tag.TX] = box.position.X;
                    jb[Tag.TY] = box.position.Y;
                    jbs.Add(jb);
                }

                JArray jps = new JArray();
                res[Tag.PLAYERS] = jps;

                if (players.Count == 0)
                {
                    JObject jp = new JObject();
                    jp[Tag.IP] = p.endpoint.Address.ToString();
                    jp[Tag.PORT] = p.endpoint.Port;
                    jp[Tag.ID] = p.id;
                    jp[Tag.NAME] = p.name;
                    jp[Tag.TX] = p.position.X;
                    jp[Tag.TY] = p.position.Y;

                    jps.Add(jp);
                    udp.SendTo(clientEndPoint, res.ToString());
                }
                else
                {
                    // response
                    foreach (Player player in players.Values)
                    {
                        JObject jp = new JObject();
                        jp[Tag.IP] = player.endpoint.Address.ToString();
                        jp[Tag.PORT] = player.endpoint.Port;
                        jp[Tag.ID] = player.id;
                        jp[Tag.NAME] = player.name;
                        jp[Tag.TX] = player.position.X;
                        jp[Tag.TY] = player.position.Y;

                        jps.Add(jp);
                    }

                    JObject jp0 = new JObject();
                    jp0[Tag.IP] = p.endpoint.Address.ToString();
                    jp0[Tag.PORT] = p.endpoint.Port;
                    jp0[Tag.ID] = p.id;
                    jp0[Tag.NAME] = p.name;
                    jp0[Tag.TX] = p.position.X;
                    jp0[Tag.TY] = p.position.Y;

                    jps.Add(jp0);
                    udp.SendTo(clientEndPoint, res.ToString());

                    //
                    JObject res2 = new JObject();
                    res2[Tag.COMMAND] = Cmd.ADD_PLAYER;
                    res2[Tag.IP] = p.endpoint.Address.ToString();
                    res2[Tag.PORT] = p.endpoint.Port;
                    res2[Tag.ID] = p.id;
                    res2[Tag.NAME] = p.name;
                    res2[Tag.TX] = p.position.X;
                    res2[Tag.TY] = p.position.Y;

                    Broadcast(res2.ToString());
                }

                players[id] = p;
            }
            else if (cmd == Cmd.DISCONNECT)
            {
                int id = (int)json[Tag.ID];
                players.Remove(id);

                JObject res = new JObject();
                res[Tag.COMMAND] = Cmd.DISCONNECT;
                res[Tag.ID] = id;

                Broadcast(res.ToString());
            }
            else if (cmd == Cmd.KEY_DOWN)
            {
                string key = (string)json[Tag.KEY];
                int id = (int)json[Tag.ID];
                keys[id][key] = true;
            }
            else if (cmd == Cmd.KEY_UP)
            {
                string key = (string)json[Tag.KEY];
                int id = (int)json[Tag.ID];
                keys[id][key] = false;
            }

        }

        private bool isPressing(int id, string key)
        {
            if (!keys[id].ContainsKey(key))
                return false;

            return keys[id][key];
        }


        private void Broadcast(string msg)
        {
            foreach (Player player in players.Values)
            {
                udp.SendTo(player.endpoint, msg);
            }
        }

        private void OnStart()
        {
            udp = new Udp(SERVER_PORT, OnReceive);
            Invalidater.Instance.Register(this);
        }

        private void OnStop()
        {
            Invalidater.Instance.Unregister(this);

            if (udp != null)
            {
                if (players.Count > 0)
                {
                    JObject res = new JObject();
                    res[Tag.COMMAND] = Cmd.SERVER_CLOSED;

                    Broadcast(res.ToString());
                }

                players.Clear();
                keys.Clear();

                udp.Close();
                udp = null;
            }
        }

        public static void Start()
        {
            Instance.OnStart();
        }

        public static void Stop()
        {
            Instance.OnStop();
        }

        private static Server instance = null;
        private static Server Instance
        {
            get
            {
                if (instance == null)
                    instance = new Server();

                return instance;
            }
        }
    }
}

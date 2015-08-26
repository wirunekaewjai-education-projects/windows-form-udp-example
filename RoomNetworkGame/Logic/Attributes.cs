using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNetworkGame.Logic
{
    public class Tag
    {
        public static string COMMAND = "COMMAND";
        public static string STATUS = "STATUS";


        public static string PLAYERS = "PLAYERS";
        public static string BOXES = "BOXES";
        
        public static string NAME = "NAME";
        public static string IP = "IP";
        public static string PORT = "PORT";
        public static string ID = "ID";

        public static string TX = "TX";
        public static string TY = "TY";


        public static string KEY = "KEY";
        public static string POSITIONS = "POSITIONS";
    }

    public class Cmd
    {
        public static int CONNECT = 1001;
        public static int DISCONNECT = 1002;
        public static int SERVER_CLOSED = 1003;

        public static int ADD_PLAYER = 1004;

        public static int KEY_DOWN = 1005;
        public static int KEY_UP = 1006;


        public static int UPDATE = 1007;
    }

    public class Status
    {
        public static int SUCCESS = 2001;
        public static int FAILED = 2002;
        
    }
}

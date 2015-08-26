using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Udp
{
    private Socket socket;
    private EndPoint destinationEndPoint;

    private Thread thread;
    private bool running;

    private Action<IPEndPoint, string> onRecv;

    public Udp(int port, Action<IPEndPoint, string> onRecv)
    {
        this.onRecv = onRecv;

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.Bind(new IPEndPoint(IPAddress.Any, port));

        thread = new Thread(Run);
        thread.Start();
    }

    public void SetDestination(string ip, int port)
    {
        IPAddress addr = IPAddress.Parse(ip);
        destinationEndPoint = new IPEndPoint(addr, port);
    }

    public void Close()
    {


        try
        {
            if (thread != null)
            {
                running = false;
                //thread.Abort();
            }
        }
        catch (ThreadAbortException)
        {

        }

        try
        {
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
        catch (ObjectDisposedException)
        {

        }
    }

    private void Run()
    {
        byte[] buffer = new byte[65535];

        running = true;
        while (running)
        {
            try
            {
                EndPoint sender = new IPEndPoint(IPAddress.Any, 0);

                int length = socket.ReceiveFrom(buffer, ref sender);
                string msg = Encoding.UTF8.GetString(buffer, 0, length);

                if (null != onRecv)
                    onRecv((IPEndPoint)sender, msg);
            }
            catch (SocketException) 
            {

            }
        }

        try
        {
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
        catch (ObjectDisposedException)
        {

        }
    }

    public void Send(string msg)
    {
        try
        {
            if (socket != null)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                socket.SendTo(buffer, destinationEndPoint);
            }
        }
        catch (ObjectDisposedException)
        {

        }
    }

    public void SendTo(IPEndPoint destination, string msg)
    {
        try
        {
            if (socket != null)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                socket.SendTo(buffer, destination);
            }
        }
        catch (ObjectDisposedException)
        {

        }
    }


}

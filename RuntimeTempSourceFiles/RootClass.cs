using System;
using System.Text;
using System.Windows.Forms;
using Microsan;

using System.Net;
using System.Net.Sockets;

using System.Threading;

//using OxyPlot;
//using OxyPlot.Series;
//using OxyPlot.WindowsForms;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO.Ports;

namespace RuntimeProgrammingNamespace
{ 
    public class RootClass
    {
        private static MainForm mForm = null;
        public static RichTextBox rtxt = null;
       
        /// <summary> The main entry point for the runtime compile code. </summary>

        public static void Main(object rootObject)
        {
            mForm                  = rootObject as MainForm;
            rtxt                   = mForm.rtxtCtrl.rtxt;
            SimpleTcpServer tcpSrv = new SimpleTcpServer(82);
            
            tcpSrv.Start();
            
        }
        
        [Shortcut("Start Test1","start")]
        public static void Test1()
        {
            MessageBox.Show("Test1");
        }
        [Shortcut("Start Test2","start2")]
        public static void Test2()
        {
            MessageBox.Show("Test2");
        }
        [Shortcut("Start Test3","stop")]
        public static void Test3()
        {
            MessageBox.Show("Test3");
        }
    }
    public class SimpleTcpServer
{
    private TcpListener listener;
    private bool running;

    public int Port { get; set; }

    public SimpleTcpServer(int port)
    {
        Port = port;
        listener = new TcpListener(IPAddress.Any, Port);
    }

    public void Start()
    {
        listener.Start();
        running = true;
        RuntimeProgrammingNamespace.RootClass.rtxt.AppendText("Server started on port " + Port);
        Console.WriteLine("Server started on port " + Port);

        Thread listenerThread = new Thread(ListenLoop);
        listenerThread.IsBackground = true;
        listenerThread.Start();
    }

    public void Stop()
    {
        running = false;
        listener.Stop();
        Console.WriteLine("Server stopped");
    }

    private void ListenLoop()
    {
        while (running)
        {
            try
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected");

                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.IsBackground = true;
                clientThread.Start();
            }
            catch (SocketException)
            {
                // Listener stopped
                break;
            }
        }
    }

    private void HandleClient(TcpClient client)
    {
        using (client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received: " + received);

                byte[] response = Encoding.UTF8.GetBytes("Echo: " + received);
                stream.Write(response, 0, response.Length);
            }

            Console.WriteLine("Client disconnected");
        }
    }
}
}
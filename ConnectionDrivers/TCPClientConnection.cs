using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace Microsan
{
    public class TCPClientConnection : IConnection
    {
        public const string TypeName = "TCP";
        public static ConnectionBase GetConnectionBase()
        {
            return new ConnectionBase
            {
                SettingsControl = TCPSettingsControl.GetConnectionSettingsControlBase(),
                GetNewConfigData = GetNewConfigData,
                Create = CreateNew
            };
        }
        public static ConnectionSettingsBase GetNewConfigData()
        {
            return new TCPSettings();
        }
        public static IConnection CreateNew()
        {
            return new TCPClientConnection();
        }

        public TCPClientConnection()
        {

        }

        public string Type => TypeName;
        public bool IsConnected => _client?.Connected ?? false;

        public event Action<byte[]> DataReceived;
        public event Action<bool> ConnectionStateChanged;

        private TcpClient _client;
        private NetworkStream _stream;
        private TCPSettings _settings;

        private Thread _recvThread;
        private volatile bool _running;

        public void Connect(ConnectionSettingsBase cfg)
        {
            _settings = cfg as TCPSettings;
            if (IsConnected)
                return;

            try
            {
                _client = new TcpClient();
                _client.Connect(_settings.Host, _settings.Port);
                _stream = _client.GetStream();

                _running = true;
                _recvThread = new Thread(ReceiveLoop)
                {
                    IsBackground = true,
                    Name = "TCP Receive Thread"
                };
                _recvThread.Start();
                ConnectionStateChanged?.Invoke(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("TCP Connect failed: " + ex.Message);
                Disconnect();
            }
        }

        public void Disconnect()
        {
            _running = false;

            try
            {
                _stream?.Close();
                _client?.Close();
            }
            catch { }

            _stream = null;
            _client = null;

            ConnectionStateChanged?.Invoke(false);
        }


        public void Send(string text)
        {
            if (!IsConnected || _stream == null)
                return;
            text = _settings.msgPrefix + text + _settings.msgPostfix;
            text = text.Replace("\\n", "\n").Replace("\\r", "\r");
            byte[] data = Encoding.UTF8.GetBytes(text);
            Send(data);
        }

        public void Send(byte[] data)
        {
            if (!IsConnected || _stream == null)
                return;

            try
            {
                _stream.Write(data, 0, data.Length);
            }
            catch
            {
                Disconnect();
            }
        }

        private void ReceiveLoop()
        {
            byte[] buffer = new byte[4096];

            try
            {
                while (_running && IsConnected)
                {
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break; // server closed connection

                    byte[] data = new byte[bytesRead];
                    Array.Copy(buffer, data, bytesRead);
                    DataReceived?.Invoke(data);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("TCP ReceiveLoop error: " + ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        public void Dispose() => Disconnect();

    }
}

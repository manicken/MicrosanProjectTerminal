using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Microsan
{
    public class WebSocketClientConnection : IConnection
    {

        public const string TypeName = "Websocket";

        public static ConnectionBase GetConnectionBase()
        {
            return new ConnectionBase
            {
                SettingsControl = WebsocketClientSettingsControl.GetConnectionSettingsControlBase(),
                GetNewConfigData = GetNewConfigData,
                Create = CreateNew
            };
        }
        public static ConnectionSettingsBase GetNewConfigData()
        {
            return new WebsocketClientSettings();
        }
        public static IConnection CreateNew()
        {
            return new WebSocketClientConnection();
        }

        public string Type => TypeName;

        private ClientWebSocket _client;
        private Thread _receiveThread;
        private bool _running;

        private WebsocketClientSettings _settings;

        public event Action<byte[]> DataReceived;
        public event Action<bool> ConnectionStateChanged;
        public event Action<string> Error;

        //public string Uri { get; set; } = "ws://127.0.0.1:8080";
        //public string MsgPrefix { get; set; } = "";
        //public string MsgPostfix { get; set; } = "\r\n";

        public bool IsConnected => _client != null && _client.State == WebSocketState.Open;

        public void Connect(ConnectionSettingsBase cfg)
        {
            _settings = cfg as WebsocketClientSettings;
            if (IsConnected) return;
            _client = new ClientWebSocket();

            try
            {
                string uri = "ws" + (_settings.UseSecure?"s":"") + "://"+ _settings.Uri;
                
                _client.ConnectAsync(new Uri(uri), CancellationToken.None).Wait();
                _running = true;
                _receiveThread = new Thread(ReceiveLoop);
                _receiveThread.IsBackground = true;
                _receiveThread.Start();
                ConnectionStateChanged?.Invoke(true);
            }
            catch (Exception ex)
            {
                Error?.Invoke("WebSocket Connect failed: " + ex.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                _running = false;
                if (_client != null)
                {
                    if (_client.State == WebSocketState.Open)
                        _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", CancellationToken.None).Wait();

                    _client.Dispose();
                }
                _client = null;
            }
            catch (Exception ex)
            {
                Error?.Invoke("WebSocket Disconnect failed: " + ex.Message);
            }
            finally
            {
                ConnectionStateChanged?.Invoke(false);
            }
        }

        public void Send(byte[] data)
        {
            if (!IsConnected) return;
            try
            {
                //string msg = _settings.msgPrefix + text + _settings.msgPostfix;
                //byte[] data = Encoding.UTF8.GetBytes(msg);
                var buffer = new ArraySegment<byte>(data);
                _client.SendAsync(buffer, WebSocketMessageType.Binary, true, CancellationToken.None).Wait();
            }
            catch (Exception ex)
            {
                Error?.Invoke("WebSocket Send failed: " + ex.Message);
            }
        }

        public void Send(string text)
        {
            if (!IsConnected) return;
            try
            {
                string msg = _settings.msgPrefix + text + _settings.msgPostfix;
                byte[] data = Encoding.UTF8.GetBytes(msg);
                var buffer = new ArraySegment<byte>(data);
                _client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None).Wait();
            }
            catch (Exception ex)
            {
                Error?.Invoke("WebSocket Send failed: " + ex.Message);
            }
        }

        private void ReceiveLoop()
        {
            byte[] buffer = new byte[2048];
            try
            {
                while (_running && _client != null && _client.State == WebSocketState.Open)
                {
                    var result = _client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).Result;
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Disconnect();
                        return;
                    }

                    string msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    DataReceived?.Invoke(Encoding.UTF8.GetBytes(msg));
                }
            }
            catch (Exception ex)
            {
                Error?.Invoke("WebSocket receive error: " + ex.Message);
                Disconnect();
            }
        }

        public void Dispose() => Disconnect();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Microsan
{
    public abstract class ConnectionSettingsBase
    {
        public string Type { get; set; } // "TCP", "Serial", etc.
        public string msgPrefix { get; set; } = "";
        public string msgPostfix { get; set; } = "\\r\\n";
    }
    
    public class ConnectionSettingsControl
    {
        public Func<Action<bool>, UserControl> Create { get; set; }
        public Action<UserControl, ConnectionSettingsBase> ApplySettings { get; set; }
        public Action<UserControl, ConnectionSettingsBase> RetrieveSettings { get; set; }

        public Action<UserControl, bool> SetConnectedState { get; set; }
    }

    public class ConnectionBase
    {
        public ConnectionSettingsControl SettingsControl { get; set; }

        public Func<IConnection> Create { get; set; }

        public Func<ConnectionSettingsBase> GetNewConfigData { get; set; }
    }

    public class ConnectionsData
    {
        public string activeType { get; set; } = "";
        public List<ConnectionSettingsBase> items { get; set; } = new List<ConnectionSettingsBase>();

        public bool GetCurrent(out ConnectionSettingsBase data)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Type == activeType)
                {
                    data = items[i];
                    return true;
                }
            }

            data = null;
            return false;
        }

        public string ToJsonString(string lineincr)
        {
            string jsonStr = lineincr + "\"connections\":";

            jsonStr += JsonConvert.SerializeObject(this, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                }
            );
            jsonStr = jsonStr.Replace("\n", "\n" + lineincr);
            return jsonStr;
        }
    }

    public interface IConnection : IDisposable
    {
        string Type { get; }                        // e.g. "Serial", "TCP", "WebSocket"
        bool IsConnected { get; }

        event Action<byte[]> DataReceived;          // Unified event for incoming data
        event Action<bool> ConnectionStateChanged;  // True = connected, False = disconnected

        void Connect(ConnectionSettingsBase cfg);   // Open the connection, with the selected settings
        void Disconnect();                          // Close the connection

        void Send(byte[] data);                     // Send raw data
        void Send(string text);

    }
}

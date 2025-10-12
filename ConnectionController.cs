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
    }
    /// <summary>
    /// used by TCP and Serial types
    /// </summary>
    public abstract class RawProtocolSettingsBase : ConnectionSettingsBase
    {
        public string msgPrefix { get; set; } = "";
        public string msgPostfix { get; set; } = "\\r\\n";
    }

    public class ConnectionBase
    {
        public Func<Action<bool>, UserControl> Create { get; set; }
        public Action<UserControl, ConnectionSettingsBase> ApplySettings { get; set; }
        public Action<UserControl, ConnectionSettingsBase> RetrieveSettings { get; set; }

        public Func<ConnectionSettingsBase> GetNewConfigData { get; set; }
        public Type SettingsType { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionController
    {
        private static Dictionary<string, ConnectionBase> Types = new Dictionary<string, ConnectionBase>()
        {
            { TCPSettingsControl.TypeName, TCPSettingsControl.GetConnectionBase() },
            { SerialSettingsControl.TypeName, SerialSettingsControl.GetConnectionBase() }
            
            //{ "Websocket", new WebsocketSettingsControl() },
            //{ "HTTP", new HttpSettingsControl() }
        };

        private int selectedInterfaceIndex = 0;

        private ConnectionsData connections;

        public ConnectionSettingsForm connectionSettingsForm = null;

        private UserControl currentCtrl;


        public ConnectionController()
        {
            connectionSettingsForm = new ConnectionSettingsForm(_ProtocolSelected, Types.Keys.ToArray());
            
        }

        public void SetData(ConnectionsData connections)
        {
            this.connections = connections;
        }

        private void _Connect(bool connectState)
        {
            // get settings from the form
            Types[connections.activeType].RetrieveSettings(currentCtrl, GetCurrentOrNewSetting());

        }
        private ConnectionSettingsBase GetCurrentOrNewSetting()
        {
            ConnectionSettingsBase data = null;
            if (connections.GetCurrent(out data) == false)
            {
                // setting did not allready existed,
                // create new
                data = Types[connections.activeType].GetNewConfigData();
                connections.items.Add(data);
            }
            return data;
        }
        private void _ProtocolSelected(string type)
        {
            connections.activeType = type;
            currentCtrl = Types[type].Create(_Connect);
            Types[type].ApplySettings(currentCtrl, GetCurrentOrNewSetting());
            connectionSettingsForm.SetControl(currentCtrl);
        }
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
}

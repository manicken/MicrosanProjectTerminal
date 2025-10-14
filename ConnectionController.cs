using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Microsan
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionController
    {
        private static Dictionary<string, ConnectionBase> Types = new Dictionary<string, ConnectionBase>()
        {
            { TCPClientConnection.TypeName, TCPClientConnection.GetConnectionBase() },
            { SerialConnection.TypeName, SerialConnection.GetConnectionBase() },
            { WebSocketClientConnection.TypeName, WebSocketClientConnection.GetConnectionBase() }
            
            //{ "HTTP", new HttpSettingsControl() }
        };

        private int selectedInterfaceIndex = 0;

        private ConnectionsData connections;

        public ConnectionSettingsForm connectionSettingsForm = null;

        private UserControl currentCtrl;

        public IConnection currentConnection = null;

        public event Action<byte[]> DataReceived;

        public ConnectionController()
        {
            connectionSettingsForm = new ConnectionSettingsForm(_ProtocolSelected, Types.Keys.ToArray());
            
        }

        public void SetData(ConnectionsData connections)
        {
            this.connections = connections;
            string[] typeNames = Types.Keys.ToArray();
            int index = -1;
            for (int i = 0; i < typeNames.Length; i++)
            {
                if (typeNames[i] == connections.activeType)
                {
                    index = i;
                    break;
                }
            }
            connectionSettingsForm.SelectProtocolByIndex(index);
            SelectSettingsControlForActiveType();
        }

        private void SaveCurrentSettings()
        {
            // retreive settings from current control
            Types[connections.activeType].SettingsControl.RetrieveSettings(currentCtrl, GetCurrentOrNewSetting());
        }

        private void SelectSettingsControlForActiveType()
        {
            ConnectionBase cb = Types[connections.activeType];
            currentCtrl = cb.SettingsControl.Create(_Connect);
            cb.SettingsControl.ApplySettings(currentCtrl, GetCurrentOrNewSetting());
            connectionSettingsForm.SetControl(currentCtrl);
        }

        private void _Connect(bool connectState)
        {
            if (connectState)
            {
                SaveCurrentSettings();
                if (currentConnection == null)
                {
                    currentConnection = Types[connections.activeType].Create();
                }
                else
                {
                    if (currentConnection.Type != connections.activeType)
                    {
                        currentConnection.DataReceived -= currentConnection_DataReceived;
                        currentConnection.Dispose();
                        currentConnection = Types[connections.activeType].Create();
                    }
                }
                currentConnection.DataReceived += currentConnection_DataReceived;
                currentConnection.ConnectionStateChanged += currentConnection_ConnectionStateChanged;
                currentConnection.Connect(GetCurrentOrNewSetting());
            }
            else
            {
                if (currentConnection != null)
                    currentConnection.Disconnect();
            }
        }

        private void currentConnection_DataReceived(byte[] data)
        {
            DataReceived?.Invoke(data);
        }
        private void currentConnection_ConnectionStateChanged(bool state)
        {
            currentCtrl.BeginInvoke((MethodInvoker)(() =>
            {
                connectionSettingsForm.SetLock(state);
                Types[connections.activeType].SettingsControl.SetConnectedState(currentCtrl, state);
            }));
        }
        public void SendToCurrentConnection(string text)
        {
            if (currentConnection == null) {
                _Connect(true);
            }
            currentConnection.Send(text);
        }
        public void SendToCurrentConnection(byte[] data)
        {
            if (currentConnection == null) {
                _Connect(true);
            }
            currentConnection.Send(data);
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
            SaveCurrentSettings();
            connections.activeType = type;
            SelectSettingsControlForActiveType();
        }

    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace Microsan
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectionController
    {
        private ConnectionsData connections;

        public ConnectionSettingsControl connectionSettingsCtrl = null;

        private UserControl currentCtrl;

        public IConnection currentConnection = null;

        public event Action<byte[]> DataReceived;

        public ConnectionController()
        {
            ConnectionRegistry.DiscoverConnections();
            connectionSettingsCtrl = new ConnectionSettingsControl(_ProtocolSelected, ConnectionRegistry.Types.Keys.ToArray());
            
        }

        public void SetData(ConnectionsData connections)
        {
            this.connections = connections;
            string[] typeNames = ConnectionRegistry.Types.Keys.ToArray();
            int index = -1;
            for (int i = 0; i < typeNames.Length; i++)
            {
                if (typeNames[i] == connections.activeType)
                {
                    index = i;
                    break;
                }
            }
            connectionSettingsCtrl.SelectProtocolByIndex(index);
            SelectSettingsControlForActiveType();
        }

        private void SaveCurrentSettings()
        {
            // retreive settings from current control
            ConnectionRegistry.Types[connections.activeType].SettingsControl.RetrieveSettings(currentCtrl, GetCurrentOrNewSetting());
        }

        private void SelectSettingsControlForActiveType()
        {
            if (ConnectionRegistry.Types.ContainsKey(connections.activeType)==false)
            {
                return;
            }
            ConnectionBase cb = ConnectionRegistry.Types[connections.activeType];
            currentCtrl = cb.SettingsControl.Create(_Connect);
            cb.SettingsControl.ApplySettings(currentCtrl, GetCurrentOrNewSetting());
            connectionSettingsCtrl.SetControl(currentCtrl);
        }

        private void _Connect(bool connectState)
        {
            if (connectState)
            {
                SaveCurrentSettings();
                if (currentConnection == null)
                {
                    currentConnection = ConnectionRegistry.Types[connections.activeType].Create();
                }
                else
                {
                    if (currentConnection.Type != connections.activeType)
                    {
                        currentConnection.DataReceived -= currentConnection_DataReceived;
                        currentConnection.Dispose();
                        currentConnection = ConnectionRegistry.Types[connections.activeType].Create();
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
                connectionSettingsCtrl.SetLock(state);
                ConnectionRegistry.Types[connections.activeType].SettingsControl.SetConnectedState(currentCtrl, state);
            }));
        }
        public void SendToCurrentConnection(SendDataItem dataItem)
        {
            if (currentConnection == null || currentConnection.IsConnected == false) {
                _Connect(true);
            }
            Dictionary<string, object> options = null;

            if (currentConnection.SupportSendOptions && !string.IsNullOrWhiteSpace(dataItem.Note))
            {
                try
                {
                    options = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataItem.Note);
                }
                catch (JsonException)
                {
                    // optional: log or show error
                    Debug.AddLine("⚠ Invalid JSON in Note, ignoring options.");
                }
            }

            if (currentConnection.SupportSendOptions && options != null)
            {
                currentConnection.Send(dataItem.Data, options);
            }
            else
            {
                currentConnection.Send(dataItem.Data);
            }
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
                data = ConnectionRegistry.Types[connections.activeType].GetNewConfigData();
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

        public static string DiscoverConnectionsAsString()
        {
            var connectionType = typeof(IConnection);
            var foundTypes = new List<string>();

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types = null;
                try
                {
                    types = asm.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    types = ex.Types.Where(t => t != null).ToArray();
                }

                foreach (var type in types)
                {
                    if (connectionType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        try
                        {
                            var instance = (IConnection)Activator.CreateInstance(type);
                            if (!string.IsNullOrEmpty(instance.Type))
                                foundTypes.Add(instance.Type);
                        }
                        catch { /* ignore instantiation failures */ }
                    }
                }
            }
            return string.Join(", ", foundTypes.Distinct().OrderBy(s => s));
        }
    }
}

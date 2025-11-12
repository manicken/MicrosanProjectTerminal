using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Microsan
{
    public class SerialConnection : IConnection
    {
        public const string TypeName = "Serial";

        public bool SupportSendOptions => false;

        public static ConnectionBase GetConnectionBase()
        {
            return new ConnectionBase
            {
                SettingsControl = SerialSettingsControl.GetConnectionSettingsControlBase(),
                GetNewConfigData = GetNewConfigData,
                Create = CreateNew
            };
        }
        public static ConnectionSettingsBase GetNewConfigData()
        {
            return new SerialSettings();
        }

        public static IConnection CreateNew()
        {
            return new SerialConnection();
        }

        public SerialConnection()
        {

        }

        public string Type => TypeName;
        public bool IsConnected => _serialPort?.IsOpen ?? false;

        public event Action<byte[]> DataReceived;
        public event Action<bool> ConnectionStateChanged;

        private SerialPort _serialPort;
        private SerialSettings _settings = new SerialSettings();

        public void Connect(ConnectionSettingsBase cfg)
        {
            _settings = cfg as SerialSettings;
            _serialPort = new SerialPort(_settings.PortName, _settings.BaudRate);
            _serialPort.DataReceived += (s, e) =>
            {
                int count = _serialPort.BytesToRead;
                byte[] buffer = new byte[count];
                _serialPort.Read(buffer, 0, count);
                DataReceived?.Invoke(buffer);
            };
            _serialPort.Open();
            ConnectionStateChanged?.Invoke(true);
        }

        public void Disconnect()
        {
            _serialPort?.Close();
            ConnectionStateChanged?.Invoke(false);
        }

        public void Send(byte[] data, Dictionary<string, object> options = null)
        {
            if (IsConnected == false) return;
            _serialPort.Write(data, 0, data.Length);
        }

        public void Send(string text, Dictionary<string, object> options = null)
        {
            text = _settings.msgPrefix + text + _settings.msgPostfix;
            text = text.Replace("\\n", "\n").Replace("\\r", "\r");
            if (IsConnected == false) return;
            byte[] data = Encoding.UTF8.GetBytes(text);
            System.Windows.Forms.MessageBox.Show(string.Join(" ",data));
            _serialPort.Write(data, 0, data.Length);
        }

        public void Dispose() => Disconnect();
    }
}

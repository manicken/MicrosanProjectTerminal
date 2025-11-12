using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsan
{
    public class MqttClientSettings : ConnectionSettingsBase
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 1883;
        public string ClientId { get; set; } = "Client1";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Topic { get; set; } = "test/topic";
        public bool UseTLS { get; set; } = false;

        public bool WithRetainFlag { get; set; } = false;

        public MqttClientSettings() { Type = "MQTT"; }
    }
}

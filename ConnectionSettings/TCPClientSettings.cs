using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsan
{
    public class TCPClientSettings : ConnectionSettingsBase
    {
        public string Host { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 8080;

        public TCPClientSettings() { Type = TCPClientConnection.TypeName; }
    }
}

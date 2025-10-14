using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsan
{
    public class SerialSettings : ConnectionSettingsBase
    {
        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;

        public SerialSettings() { Type = SerialConnection.TypeName; }
    }
}

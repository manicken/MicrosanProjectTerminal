using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsan
{
    public class WebsocketClientSettings : RawProtocolSettingsBase
    {
        public string Uri { get; set; } = "127.0.0.1:3000";

        public bool UseSecure { get; set; } = false;

        public WebsocketClientSettings() { Type = "Websocket"; }
    }
}

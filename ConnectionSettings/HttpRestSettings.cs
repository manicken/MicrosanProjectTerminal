using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsan
{
    public class HttpRestSettings : ConnectionSettingsBase
    {
        public string Uri { get; set; } = "127.0.0.1";

        public bool UseSecure { get; set; } = false;

        public HttpRestSettings() { Type = HttpRestClientConnection.TypeName; msgPostfix = ""; msgPrefix = ""; }
    }
}

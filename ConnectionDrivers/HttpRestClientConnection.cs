using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsan
{
    public class HttpRestClientConnection : IConnection
    {
        public const string TypeName = "HttpRest";

        public bool SupportSendOptions => false;

        public static ConnectionBase GetConnectionBase()
        {
            return new ConnectionBase
            {
                SettingsControl = HttpRestSettingsControl.GetConnectionSettingsControlBase(),
                GetNewConfigData = GetNewConfigData,
                Create = CreateNew
            };
        }
        public static ConnectionSettingsBase GetNewConfigData()
        {
            return new HttpRestSettings();
        }
        public static IConnection CreateNew()
        {
            return new HttpRestClientConnection();
        }

        private HttpClient _client = new HttpClient();

        public string Type => TypeName;
        public bool IsConnected => true; // HTTP is stateless, so "connected" just means usable

        public event Action<byte[]> DataReceived;
        public event Action<bool> ConnectionStateChanged;

        HttpRestSettings _settings = new HttpRestSettings();

        public void Connect(ConnectionSettingsBase cfg)
        {
            _settings = cfg as HttpRestSettings;
            // maybe just validate base URL here
            ConnectionStateChanged?.Invoke(true);
        }

        public void Disconnect()
        {
            ConnectionStateChanged?.Invoke(false);
        }

        public async void Send(string url, Dictionary<string, object> options = null)
        {
            //url = "http" + (_settings.UseSecure ? "s" : "") + "://" + _settings.Uri + (url.StartsWith("/")?"":"/") + url;
            // Build full URL
            string fullUrl = $"http{(_settings.UseSecure ? "s" : "")}://{_settings.Uri.TrimEnd('/')}/{url.TrimStart('/')}";

            try
            {
                var response = await _client.GetAsync(fullUrl);
                string body = await response.Content.ReadAsStringAsync();
                DataReceived?.Invoke(Encoding.UTF8.GetBytes(body));
                ConnectionStateChanged?.Invoke(false);
            }
            catch (Exception ex)
            {
                string err = $"HTTP Error: {ex.Message}";
                DataReceived?.Invoke(Encoding.UTF8.GetBytes(err));
            }
        }

        public void Send(byte[] data, Dictionary<string, object> options = null) => Send(Encoding.UTF8.GetString(data), options);

        public void Dispose() => _client.Dispose();
    }

}

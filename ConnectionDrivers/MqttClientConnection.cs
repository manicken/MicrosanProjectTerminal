using System;
using System.Collections.Generic;
using System.Text;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace Microsan
{
    public class MqttClientConnection : IConnection
    {
        public const string TypeName = "MQTT";
        public bool SupportSendOptions => true;
        public string Type => TypeName;
        public bool IsConnected => _client?.IsConnected == true;

        public event Action<byte[]> DataReceived;
        public event Action<bool> ConnectionStateChanged;

        private IMqttClient _client;
        private IMqttClientOptions _options;
        private MqttClientSettings _settings;

        public static ConnectionBase GetConnectionBase()
        {
            return new ConnectionBase
            {
                SettingsControl = MqttClientSettingsControl.GetConnectionSettingsControlBase(),
                GetNewConfigData = () => new MqttClientSettings(),
                Create = () => new MqttClientConnection()
            };
        }

        public void Connect(ConnectionSettingsBase cfg)
        {
            _settings = cfg as MqttClientSettings ?? new MqttClientSettings();

            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            _client.UseConnectedHandler(e => ConnectionStateChanged?.Invoke(true));
            _client.UseDisconnectedHandler(e => ConnectionStateChanged?.Invoke(false));
            _client.UseApplicationMessageReceivedHandler(e =>
            {
                DataReceived?.Invoke(e.ApplicationMessage.Payload);
            });

            var builder = new MqttClientOptionsBuilder()
                .WithClientId(_settings.ClientId)
                .WithTcpServer(_settings.Host, _settings.Port);

            if (!string.IsNullOrEmpty(_settings.Username))
                builder.WithCredentials(_settings.Username, _settings.Password);

            if (_settings.UseTLS)
                builder.WithTls();

            _options = builder.Build();
            _client.ConnectAsync(_options).Wait();

            // Subscribe to the configured topic
            _client.SubscribeAsync(_settings.Topic).Wait();
        }

        public void Disconnect()
        {
            if (_client?.IsConnected == true)
            {
                _client.DisconnectAsync().Wait();
                ConnectionStateChanged?.Invoke(false);
            }
        }

        public void Send(byte[] data, Dictionary<string, object> options = null)
        {
            Send(Encoding.UTF8.GetString(data), options);
        }

        public void Send(string text, Dictionary<string, object> options = null)
        {
            if (_client?.IsConnected == true)
            {
                string topic = _settings.Topic; // fallback default
                if (options != null && options.ContainsKey("topic"))
                {
                    topic = options["topic"].ToString();
                }

                bool withRetainFlag = _settings.WithRetainFlag;
                if (options != null && options.TryGetValue("retain", out object retainObj))
                {
                    withRetainFlag = Convert.ToBoolean(retainObj);
                }


                var msg = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(text)
                    .WithRetainFlag(withRetainFlag)
                    .Build();
                _client.PublishAsync(msg).Wait();
            }
        }

        public void Dispose()
        {
            try { _client?.Dispose(); } catch { }
        }
    }
}

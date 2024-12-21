using Lumos.Producer.Helpers;
using MQTTnet;
using MQTTnet.Client;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Lumos.Producer.MPDLMessageData;

namespace Lumos.Producer
{
    internal class MQTTProducer(MqttFactory factory) : IMQTTProducer
    {
        private readonly MqttFactory factory = factory;
        private IMqttClient client;
        private MqttClientOptions mqttOptions;
        private MqttClientSubscribeOptions mqttSubscribeOptions;

        public async Task Produce(IServiceProvider serviceProvider)
        {
            client = factory.CreateMqttClient();
            //TODO: handle with appsetting.json -------SERGEN
            mqttOptions = new MqttClientOptionsBuilder()
                              .WithTcpServer("108.142.114.17", 17500).Build();
            mqttSubscribeOptions = factory.CreateSubscribeOptionsBuilder()
                                                        .WithTopicFilter(f => { f.WithTopic($"solarbita/db8fe608-269b-41b3-8e31-e60479f3bdf7"); })
                                                        .Build();
            client.ApplicationMessageReceivedAsync += Invoke;
            client.ConnectedAsync += OnConnected;
            client.DisconnectedAsync += OnDisconnected;
            await client.ConnectAsync(mqttOptions, CancellationToken.None);
        }

        private async Task OnDisconnected(MqttClientDisconnectedEventArgs args)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            await client.ConnectAsync(mqttOptions, CancellationToken.None);
        }

        private async Task OnConnected(MqttClientConnectedEventArgs args)
        {
            await client.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

        private async Task Invoke(MqttApplicationMessageReceivedEventArgs args)
        {
            // Unzip and deserialize data package
            var options = new JsonSerializerOptions { NumberHandling = JsonNumberHandling.AllowReadingFromString };
            using var stream = new MemoryStream(args.ApplicationMessage.PayloadSegment.Array);
            using var zipStream = new GZipStream(stream, CompressionMode.Decompress);
            using var output = new MemoryStream();
            await zipStream.CopyToAsync(output);
            output.Position = 0;
            using var reader = new StreamReader(output);
            var json = await reader.ReadToEndAsync();
            options.Converters.Add(new CustomDateTimeConverter("dd/MM/yyyy HH:mm:ss"));
            var message = JsonSerializer.Deserialize<MPDLMessage>(json, options);
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(JsonSerializer.Serialize(message));
        }
    }
}
using Lumos.Data.Context;
using Lumos.Data.Entities;
using Lumos.Producer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Lumos.Producer.Models.MPDLMessageData;

namespace Lumos.Producer
{
    internal class MQTTProducer(MqttFactory factory) : IMQTTProducer
    {
        private readonly MqttFactory factory = factory;
        private LumosDbContext dbContext;
        private IMqttClient client;
        private MqttClientOptions mqttOptions;
        private MqttClientSubscribeOptions mqttSubscribeOptions;
        private Guid stationId;
        public async Task Produce(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            dbContext = scope.ServiceProvider.GetService<LumosDbContext>();

            var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory) 
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
            var address = config["mqtt:address"];
            var port = config["mqtt:port"];
            var clusterId = config["mqtt:clusterId"];
            stationId = Guid.Parse(config["mqtt:stationId"]);
            //
            client = factory.CreateMqttClient();
            //TODO: handle with appsetting.json -------SERGEN
            mqttOptions = new MqttClientOptionsBuilder()
                              .WithTcpServer(address, int.Parse(port)).Build();
            mqttSubscribeOptions = factory.CreateSubscribeOptionsBuilder()
                                                        .WithTopicFilter(f => { f.WithTopic($"solarbita/{clusterId}"); })
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
            var stationId = new Guid("1fafd91a-7601-4ab4-8fab-a2e34669fed9");
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
            // mapp and save
            var stationData = message.Stations.First(x => x.Id == stationId);
            var mappedData = await HandleDeviceMapping(stationData);

            var entity = new InverterData
            {
                Id = Guid.NewGuid(),
                Date = stationData.Date,
                StationId = stationId,
                DeviceDatas = mappedData
            };

            dbContext.InverterDatas.Add(entity);
            await dbContext.SaveChangesAsync();

        }

        private async Task<List<ProductionData>> HandleDeviceMapping(MPDLStation stationData)
        {
            var devicetagMappings = await dbContext.DeviceTagMappings.ToListAsync();
            var mappedData = new List<ProductionData>();
            foreach (var device in stationData.Devices)
            {
                var mappings = device.Mappings
                                     .Where(x => devicetagMappings.Select(x => x.Address).Contains(x.Address))
                                     .Select(s => new ProductionData()
                                     {
                                         Value = s.Value,
                                         DeviceId = Guid.Parse(device.DeviceId),
                                         TagId = devicetagMappings.First(x => x.DeviceId == Guid.Parse(device.DeviceId) && x.Address == s.Address).TagId,
                                     })
                                     .ToList();

                mappedData.AddRange(mappings);
            }
            return mappedData;
        }
    }
}
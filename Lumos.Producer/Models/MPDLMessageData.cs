using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace Lumos.Producer.Models
{
    public class MPDLMessageData
    {
        public class Device
        {
            [JsonPropertyName("did")]
            public string DeviceId { get; set; }

            [JsonPropertyName("tags")]
            public List<Mapping> Mappings { get; set; }
        }

        public class Mapping
        {
            [JsonPropertyName("a")]
            public int Address { get; set; }

            [JsonPropertyName("v")]
            public decimal Value { get; set; }
        }

        public class MPDLMessage
        {
            [JsonPropertyName("datalog_id")]
            public string Id { get; set; }

            [JsonPropertyName("datalog_imei")]
            public string IMEI { get; set; }

            [JsonPropertyName("datalog_mac")]
            public string MacAddress { get; set; }

            [JsonPropertyName("stations")]
            public List<MPDLStation> Stations { get; set; }
        }

        public class MPDLStation
        {
            [JsonPropertyName("dt")]
            public DateTime Date { get; set; }

            [JsonPropertyName("devices")]
            public List<Device> Devices { get; set; }

            [JsonPropertyName("sid")]
            public Guid Id { get; set; }
        }
    }
}
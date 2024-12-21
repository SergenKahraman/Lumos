using System.Diagnostics.CodeAnalysis;

namespace Lumos.Producer
{
    public class MappedData
    {
        public MappedData(int tagAddress)
        {
            TagAddress = tagAddress;
        }

        public int TagAddress { get; set; }
        public decimal? Value { get; set; }
        public Guid? DeviceId { get; set; }
        public string IpAddress { get; set; }
        public int? Port { get; set; }
        public bool IsString { get; set; }
    }

    public class MappedDataComparer : IEqualityComparer<MappedData>
    {
        public bool Equals(MappedData x, MappedData y)
        {
            return x.TagAddress == y.TagAddress && x.DeviceId == y.DeviceId && x.IpAddress == y.IpAddress && x.Port == y.Port;
        }

        public int GetHashCode([DisallowNull] MappedData obj)
        {
            if (obj.DeviceId.HasValue)
            {
                return (obj.TagAddress + obj.DeviceId.Value.ToString() + obj.IpAddress + obj.Port.ToString()).GetHashCode();
            }
            return (obj.TagAddress + obj.IpAddress + obj.Port.ToString()).GetHashCode();
        }
    }
}
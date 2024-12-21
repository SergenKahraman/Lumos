using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Entities
{
    [Table("InverterDatas")]
    public class InverterData : EntityBase
    {
        [Required]
        public DateTimeOffset Date { get; set; }

        [Required]
        public Guid StationId { get; set; }

        [Column(TypeName = "jsonb")]
        public List<ProductionData> DeviceDatas { get; set; }

        [ForeignKey(nameof(StationId))]
        public Station Station { get; set; }
    }

    public class ProductionData
    {
        public Guid DeviceId { get; set; }
        public Guid TagId { get; set; }
        public decimal Value { get; set; }
    }
}
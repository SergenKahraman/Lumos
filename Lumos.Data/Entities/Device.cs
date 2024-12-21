using Lumos.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Entities
{
    [Table("Devices")]
    public class Device : EntityBase
    {
        [Required]
        public Guid StationId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public DeviceType Type { get; set; }
        public int? Order { get; set; }

        [ForeignKey(nameof(StationId))]
        public Station Station { get; set; }
    }
}
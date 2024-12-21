using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Entities
{
    [Table("DeviceTagMappings")]
    public class DeviceTagMapping : EntityBase
    {
        [Required]
        public Guid DeviceId { get; set; }

        [Required]
        public Guid TagId { get; set; }

        [Required]
        public int Address { get; set; }

        public decimal? Factor { get; set; }

        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }

        [ForeignKey(nameof(DeviceId))]
        public Device Device { get; set; }
    }
}
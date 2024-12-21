using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Data.Entities
{
    [Table("Tags")]
    public class Tag : EntityBase
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.Data.Entities
{
    [Table("Stations")]
    public class Station : EntityBase
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }
    }
}
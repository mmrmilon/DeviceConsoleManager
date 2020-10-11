using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
    [Table("prefix")]
    public class Prefix : Entity
    {
        [Column("prefix_level")]
        public string PrefixLevel { get; set; }

        [Column("serial_no")]
        public int SerailNo { get; set; }
    }
}

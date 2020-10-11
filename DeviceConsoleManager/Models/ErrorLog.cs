using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
    [Table("error_log")]
    public class ErrorLog : Entity
    {
        [Column("even_name")]
        public string EvenName { get; set; }

        [Column("error_details")]
        public string ErrorDetails { get; set; }
    }
}

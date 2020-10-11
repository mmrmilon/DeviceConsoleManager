using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	[Table("transations")]
	public class Transictions:Entity
	{
        [Column("guid")]
        public string Guid { get; set; }

        [Column("transiction_date")]
		public DateTime TransictionDate { get; set; }

		[Column("transiction_amount")]
		public double TransictionAmount { get; set; }

		[Column("service_id")]
		public long ServiceId { get; set; } 
		
	}
}

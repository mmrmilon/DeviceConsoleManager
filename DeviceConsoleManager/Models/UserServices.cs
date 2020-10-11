using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	[Table("user_services")]
	public class UserServices:Entity
	{
        [Column("guid")]
        public string Guid { get; set; }

        [Column("device_id")]
		public long DeviceId { get; set; }

		[Column("user_id")]
		public long UserId { get; set; }

		[Column("started_time")]
		public DateTime StartTime { get; set; }

		[Column("end_time")]
		public DateTime EndTime { get; set; }

		[Column("total_amount")]
		public double TotalAmount { get; set; }
		
	}
}

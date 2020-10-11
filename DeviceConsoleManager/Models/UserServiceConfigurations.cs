using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	[Table("user_service_configurations")]
	public class UserServiceConfigurations:Entity
	{
        [Column("guid")]
        public string Guid { get; set; }

        [Column("device_id")]
        public long DeviceId { get; set; }

        [Column("unit")]
		public double Unit { get; set; }

		[Column("unit_rate")]
		public double UnitRate { get; set; }
	}
}

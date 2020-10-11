using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	[Table("device_serial_number")]
	public class DeviceSerialNumber:Entity
	{
		[Column("serial_number")]
		public string SerialNumber { get; set; }

		[Column("is_used")]
		public bool IsUsed { get; set; }

	}
}

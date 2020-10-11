using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	[Table("device_locations")]
	public class DeviceLocations:Entity
	{
        [Column("guid")]
        public string Guid { get; set; }

        [Column("location_name")]
		public string LocationName { get; set; }

		[Column("location_level")]
		public string LocationLevel { get; set; }	

	}
}

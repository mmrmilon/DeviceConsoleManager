using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	[Table("user_groups")]
	public class UserGroups:Entity
	{
        [Column("guid")]
        public string Guid { get; set; }

        [Column("device_id")]
        public long DeviceId { get; set; }

        [Column("user_id")]
		public long UserId { get; set; }

	}
}

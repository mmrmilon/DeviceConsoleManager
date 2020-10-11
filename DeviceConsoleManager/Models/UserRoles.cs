using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	[Table("user_roles")]
	public class UserRoles:Entity
	{
        [Column("guid")]
        public string Guid { get; set; }

        [Column("role_name")]
		public string RoleName { get; set; }

	}
}

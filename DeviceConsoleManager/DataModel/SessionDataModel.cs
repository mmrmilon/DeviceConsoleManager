using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.DataModel
{
	public class SessionDataModel
	{
		public long UserId { get; set; }

		public string UserName { get; set; }

		public string UserRole { get; set; }

		public long UserRoleId { get; set; }

		public string EmailAddress { get; set; }
	}
}

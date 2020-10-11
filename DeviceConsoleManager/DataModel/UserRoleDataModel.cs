using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.DataModel
{
	public class UserRoleDataModel
	{
		public long Id { get; set; }

		public string FristName { get; set; }

		public string LastName { get; set; }

		public string EmailAddress { get; set; }

		public string MobileNumber { get; set; }

		public string UserName { get; set; }

		public Guid? Guid { get; set; }

		public string RoleName { get; set; }

		public bool IsActive { get; set; }

	}
}

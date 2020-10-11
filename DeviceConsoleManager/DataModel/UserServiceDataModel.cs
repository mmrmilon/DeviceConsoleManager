using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.DataModel
{
	public class UserServiceDataModel
	{
		public Users User { get; set; }

		public UserRoles UserRole { get; set; }

		public UserServices UserService { get; set; }

		public Transictions Transiction { get; set; }

		public DeviceInformations DeviceInformation { get; set; }

		public DeviceLocations DeviceLocation { get; set; }

        public DeviceStatus DeviceStatus { get; set; }
    }
}

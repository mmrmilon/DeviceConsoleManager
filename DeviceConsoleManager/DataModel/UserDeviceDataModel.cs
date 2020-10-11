using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.DataModel
{
	public class UserDeviceDataModel
	{
        public Users User { get; set; }

        public UserRoles UserRole { get; set; }

        public UserCards UserCard { get; set; }

		public List<DeviceInformations> DeviceInformation { get; set; }
    }
}

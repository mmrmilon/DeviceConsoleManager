using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.DataModel
{
	public class DeviceInformationsDataModel
	{
		public long Id { get; set; }

		public string DeviceCode { get; set; }
		
		public string DeviceName { get; set; }
		
		public long DeviceLocationId { get; set; }

		public string DeviceLocationName { get; set; }
		
		public string DeivceToken { get; set; }
	}
}

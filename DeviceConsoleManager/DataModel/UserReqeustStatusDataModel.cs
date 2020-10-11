using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.DataModel
{
	public class UserReqeustStatusDataModel
	{
		public bool IsSuccess { get; set; }

		public double AvailableBalance { get; set; }

		public double Unit { get; set; }

		public double UnitRate { get; set; }

		public bool IsVaildUser { get; set; }

        public string CustomerName { get; set; }
	}
}

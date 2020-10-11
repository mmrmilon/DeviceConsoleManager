using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.DataModel
{
	public class UserAccountDataModel
	{

		public long Id { get; set; }

		public long UserId { get; set; }

		public string UserName { get; set; }

		public string AccountNumber { get; set; }

		public double AvailableBalance { get; set; }
	}
}

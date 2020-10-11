using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.DataModel
{
	public class CardUserDataModel
	{
		public long UserCardId { get; set; }

		public long UserId { get; set; }

		public string UserName { get; set; }

		public Nullable<DateTime> ExpiryDate { get; set; }

		public string ChipCardNo { get; set; }

        public UserCards Card { get; set; }

        public Users User { get; set; }
    }
}

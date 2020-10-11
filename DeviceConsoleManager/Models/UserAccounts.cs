using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeviceConsoleManager.Models
{
	[Table("user_accounts")]
	public class UserAccounts:Entity
	{
        [Column("guid")]
        public string Guid { get; set; }

        [Column("user_id")]
		public long UserId { get; set; }

		[Column("account_number")]
		public string AccountNumber { get; set; }

		[Column("available_balance")]
		public double AvailableBalance { get; set; }
    }
}

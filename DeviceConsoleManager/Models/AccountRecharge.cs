using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	[Table("account_recharges")]
	public class AccountRecharge : Entity
	{
        [Column("guid")]
        public string Guid { get; set; }

        [Column("user_id")]
		public long UserId { get; set; }

		[Column("from_account")]
		public string FromAccount { get; set; }

		[Column("transiction_id")]
		public string TransictionId { get; set; }

        [Column("payment_method_id")]
        public long PaymentMethodId { get; set; }        

        [Column("recharge_amount")]
		public double RechargeAmount { get; set; }
	}
}

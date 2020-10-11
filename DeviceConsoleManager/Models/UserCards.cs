using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	[Table("user_chipcards")]
	public class UserCards:Entity
	{
        [Column("guid")]
        public string Guid { get; set; }

        [Column("chip_card_no")]
		public string ChipCardNo { get; set; }

		[Column("expiry_date")]
		public DateTime ExpairDate { get; set; }

		[Column("is_block")]
		public bool IsBlocked { get; set; }

		[Column("user_id")]
		public long UserId { get; set; }

	}
}


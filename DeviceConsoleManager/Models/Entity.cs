using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	public class Entity
	{
		[Key]
		[Column("id")]
		public long Id { get; set; }

		[Column("created_at")]
		public Nullable<DateTime> CreatedOn { get; set; }

		[Column("updated_at")]
		public Nullable<DateTime> UpdatedOn { get; set; }

		[Column("is_active")]
		public bool IsActive { get; set; }
	}
}

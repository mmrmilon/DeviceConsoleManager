using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	[Table("payment_methods")]
    public class PaymentMethods : Entity
    {
        [Column("method_name")]
        public string MethodName { get; set; }
    }
}

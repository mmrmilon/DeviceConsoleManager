using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.DataModel
{
    public class AccountRechargeDataModel
    {
        public AccountRecharge AccountRecharge { get; set; }

        public Users User { get; set; }

        public PaymentMethods PaymentMethod { get; set; }
    }
}

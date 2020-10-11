using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
    public interface IPaymentMethodRepository
    {
        List<PaymentMethods> GetAll();

        PaymentMethods Save(PaymentMethods model);

        PaymentMethods Update(PaymentMethods model);

        bool Delete(long id);
    }
}

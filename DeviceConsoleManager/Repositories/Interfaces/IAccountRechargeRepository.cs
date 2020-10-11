using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using System.Collections.Generic;

namespace DeviceConsoleManager.Repositories.Interfaces
{
    public interface IAccountRechargeRepository
    {
        List<AccountRechargeDataModel> GetAll();

        AccountRecharge Save(AccountRecharge model);

        AccountRecharge Update(AccountRecharge model);

        bool Delete(long id);
    }
}

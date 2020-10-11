using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
	public interface IUserAccountRepository
	{
		List<UserAccountDataModel> GetAll();

		long Save(UserAccounts userAccounts);

		bool Update(UserAccounts userAccounts);

		bool Delete(UserAccounts userAccounts);

		List<Users> GetAllUsers();

	}
}

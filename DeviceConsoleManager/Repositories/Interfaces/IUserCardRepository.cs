using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
	public interface IUserCardRepository
	{
		List<CardUserDataModel> GetCardUsers();

		bool WriteCardUsers(UserCards usersCard);

		List<Users> GetAllUsersNotHasCard();

        bool Update(UserCards card);

        bool Delete(long Id);

    }
}

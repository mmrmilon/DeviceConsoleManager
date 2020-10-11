using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
	public interface IUserRepository
	{
		SessionDataModel Login(string userName, string password);

		List<UserRoleDataModel> GetAll(string roleName);

        List<UserRoleDataModel> GetAllCustomer();

        bool SaveUserAndAddAccount(Users users);

		bool Update(Users user);

		long Save(Users user);

		bool Delete(long userId);

		bool IsDuplicateEmail(string emailAddress);

		bool ResetPassword(string password,long userId);

		List<Users> GetAllListOfUsers();

		SessionDataModel GetAuthenticUser(string emailAddress, string password);


	}
}

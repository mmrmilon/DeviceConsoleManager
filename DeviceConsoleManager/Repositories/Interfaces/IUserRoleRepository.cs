using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
	public interface IUserRoleRepository
	{
		List<UserRoles> GetAll();

		bool Add(UserRoles userRole);

		bool Update(UserRoles userRoles);

		bool Delete(long Id);

		List<UserRoles> GetAll(string roleName);
	}
}

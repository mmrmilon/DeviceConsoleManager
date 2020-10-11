using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using DeviceConsoleManager.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Implementation
{
	public class UserRoleRepository:IUserRoleRepository
	{
		private readonly DatabaseContext context;

		public UserRoleRepository(DatabaseContext context)
		{
			this.context = context;
		}

		public bool Add(UserRoles userRoles)
		{
            userRoles.Guid = Guid.NewGuid().ToString().ToUpper();
            userRoles.UpdatedOn = DateTime.Now;
			userRoles.CreatedOn = DateTime.Now;
			userRoles.IsActive = true;
			context.Add(userRoles);
			var save = context.SaveChanges();

			return save > 0;
		}

		public bool Delete(long Id)
		{
			var user = context.UserRole.Where(x => x.Id == Id).FirstOrDefault();
			context.Entry(user).State = EntityState.Deleted;
			var result = context.SaveChanges();
			return result > 0;
		}

		public List<UserRoles> GetAll()
		{
			var result = (from ur in context.UserRole
						  orderby ur.Id descending
						  select new UserRoles
						  {
							  Id = ur.Id,
							  RoleName = ur.RoleName

						  }).ToList();

			return result;
		}

		public List<UserRoles> GetAll(string roleName)
		{
			var result = (from ur in context.UserRole
						  orderby ur.Id descending
						  select new UserRoles
						  {
							  Id = ur.Id,
							  RoleName = ur.RoleName

						  }).AsEnumerable();

			if (roleName.Equals(Constants.SUPERADMIN))
				return result.Where(x => x.RoleName.Equals(Constants.ADMIN)).ToList();
			else if (roleName.Equals(Constants.ADMIN))
				return result.Where(x => x.RoleName.Equals(Constants.OPERATOR)).ToList();
			else
				return result.Where(x => x.RoleName.Equals(Constants.CUSTOMER)).ToList();
		}

		public bool Update(UserRoles userRoles)
		{
			userRoles.UpdatedOn = DateTime.Now;
			context.Entry(userRoles).State = EntityState.Modified;
			var result = context.SaveChanges();
			return result > 0;
		}
	}
}

using DeviceConsoleManager.DataModel;
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
	public class UserDeviceRepository : IUserDeviceRepository
	{
		private DatabaseContext context;

		public UserDeviceRepository(DatabaseContext context)
		{
			this.context = context;
		}

        public bool Add(IEnumerable<UserGroups> model, long userId)
        {
            try
            {
                if (Delete(userId))
                {
                    foreach (var item in model)
                    {
                        item.Guid = Guid.NewGuid().ToString().ToUpper();
                        item.CreatedOn = DateTime.Now;
                        item.IsActive = true;

                        context.Add(item);
                        context.SaveChanges();
                    }

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

		public bool Delete(long userId)
		{
            try
            {
                var records = context.UserGroups.Where(x => x.UserId == userId).ToList();
                foreach (var item in records)
                {
                    context.Entry(item).State = EntityState.Deleted;
                    var result = context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
		}

		public List<UserDeviceDataModel> GetAll()
		{
            var records = (from u in context.User
                        join ur in context.UserRole on u.RoleId equals ur.Id
                        join uc in context.UserCards on u.Id equals uc.UserId
                        select new UserDeviceDataModel
                        {
                            User = u,
                            UserRole = ur,
                            UserCard = uc,
                            DeviceInformation = null
                        }).AsEnumerable().Where(x => x.UserRole.RoleName.ToUpper().Equals(Constants.CUSTOMER.ToUpper())).ToList();
            foreach(var item in records)
            {
                item.DeviceInformation = (from ug in context.UserGroups.Where(x => x.UserId == item.User.Id)
                                          join d in context.DeviceInformations on ug.DeviceId equals d.Id
                                          select d).ToList();
            }

			return records;
		}

		public List<Users> GetAllUsers()
		{
			var result = (
				from u in context.User
				join p in context.UserGroups on u.Id equals p.UserId into ps
				from p in ps.DefaultIfEmpty()
				where p == null
				select new Users
				{
					Id = u.Id,
					UserName = u.UserName
				}).ToList();

			return result;
		}

        public List<DeviceInformations> GetUserDeviceBy(long userId)
        {
            var result = (from ug in context.UserGroups.Where(x => x.UserId == userId)
                        join d in context.DeviceInformations on ug.DeviceId equals d.Id
                        select d).ToList();

            return result;
        }

        public bool Update(UserDeviceDataModel userdevice)
		{
			throw new NotImplementedException();
		}

        public IEnumerable<UserDeviceMappingDataModel> GetUserDeviceMapping(long userId) {
            var result = (from d in context.DeviceInformations
                          join dl in context.DeviceLocation on d.DeviceLocationId equals dl.Id
                          join ug in context.UserGroups.Where(x => x.UserId == userId) on d.Id equals ug.DeviceId into tempUserGroup
                          from ug in tempUserGroup.DefaultIfEmpty()
                          select new UserDeviceMappingDataModel
                          {
                              DeviceInformation = d,
                              DeviceLocation = dl,
                              UserGroup = ug,
                              UserId = userId,
                              IsChecked = ug == null ? false : true
                          }).ToList();

            return result;
        }
	}
}

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
	public class UserRepository : IUserRepository
	{
		private readonly DatabaseContext context;

		//private IDeviceRepository deviceRepository { get; set; }


		public UserRepository(DatabaseContext context)
		{
			this.context = context;
		}

		public SessionDataModel Login(string emailAddress, string password)
		{
			password = Constants.GetSecurePassword(password);

			var result = (from u in context.User.Where(x => x.EmailAddress == emailAddress && x.Password == password)
						  join ur in context.UserRole on u.RoleId equals ur.Id
						  select new SessionDataModel
						  {
							  UserRole = ur.RoleName,
							  UserId = u.Id,
							  UserRoleId = ur.Id,
							  EmailAddress = u.EmailAddress,
							  UserName = u.UserName

						  }).FirstOrDefault();

			return result;

		}

		public SessionDataModel GetAuthenticUser(string emailAddress, string password)
		{
			password = Constants.GetSecurePassword(password);

			var result = (from u in context.User.Where(x => x.EmailAddress == emailAddress && x.Password == password)
						  join ur in context.UserRole on u.RoleId equals ur.Id
						  where ur.RoleName == Constants.OPERATOR
						  select new SessionDataModel
						  {
							  UserRole = ur.RoleName,
							  UserId = u.Id,
							  UserRoleId = ur.Id,
							  EmailAddress = u.EmailAddress,
							  UserName = u.UserName

						  }).FirstOrDefault();

			return result;

		}

		public List<UserRoleDataModel> GetAll(string userRole)
		{
			var result = (from u in context.User
						  join ur in context.UserRole on u.RoleId equals ur.Id
						  select new UserRoleDataModel
						  {
							  Id = u.Id,
							  EmailAddress = u.EmailAddress,
							  UserName = u.UserName,
							  FristName = u.FristName,
							  LastName = u.LastName,
							  // Guid = (Guid?)u.Guid,
							  MobileNumber = u.MobileNumber,
							  RoleName = ur.RoleName,
							  IsActive = u.IsActive
						  }).AsEnumerable();

			if (userRole.Equals(Constants.SUPERADMIN))
				return result.Where(x => x.RoleName.Equals(Constants.ADMIN)).ToList();
			else if(userRole.Equals(Constants.ADMIN))
				return result.Where(x => x.RoleName.Equals(Constants.OPERATOR)).ToList();
			else 
				return result.Where(x => x.RoleName.Equals(Constants.CUSTOMER)).ToList();
			
		}

        public List<UserRoleDataModel> GetAllCustomer()
        {
            var result = (from u in context.User
                          join ur in context.UserRole on u.RoleId equals ur.Id
                          select new UserRoleDataModel
                          {
                              Id = u.Id,
                              UserName = u.UserName,
                              FristName = u.FristName,
                              LastName = u.LastName,
                              EmailAddress = u.EmailAddress,
                              MobileNumber = u.MobileNumber,
                              RoleName = ur.RoleName,
                              IsActive = u.IsActive
                          }).AsEnumerable().Where(x => x.RoleName.Equals(Constants.CUSTOMER)).ToList();

            return result;
        }


        public long Save(Users users)
		{
            users.Guid = Guid.NewGuid().ToString().ToUpper();
			users.CreatedOn = DateTime.Now;
			users.UserName = users.FristName + " " + users.LastName;
			users.Password = Constants.GetSecurePassword(users.Password);
			context.Add(users);
			context.SaveChanges();

			return users.Id;
		}
		public bool SaveUserAndAddAccount(Users users)
		{
			var userId = this.Save(users);
			this.AddToUserAccount(userId, users.MobileNumber, 1000);
			return true;
		}

		public bool Update(Users users)
		{

			var user = context.User.Where(x => x.Id == users.Id).FirstOrDefault();


			user.UpdatedOn = DateTime.Now;
			user.UserName = users.FristName + " " + users.LastName;
			user.FristName = users.FristName;
			user.LastName = users.LastName;
			user.EmailAddress = users.EmailAddress;
			user.MobileNumber = users.MobileNumber;

			context.Entry(user).State = EntityState.Modified;
			var result= context.SaveChanges();
			return result > 0;
		}

		public bool Delete(long Id)
		{
			var user = context.User.Where(x => x.Id == Id).FirstOrDefault();
			context.Entry(user).State = EntityState.Deleted;
			this.DeleteUserCardByUserId(Id);
			this.DeleteUserDeviceByUserId(Id);
			var result = context.SaveChanges();
			return result > 0;
		}

		private void DeleteUserDeviceByUserId(long userId)
		{
			var userGroup = context.UserGroups.Where(x => x.UserId == userId).FirstOrDefault();
			if(userGroup != null)
				context.Entry(userGroup).State = EntityState.Deleted;
		}
		private void DeleteUserCardByUserId(long userId)
		{
			var userCards = context.UserCards.Where(x => x.UserId == userId).FirstOrDefault();
			if (userCards != null)
				context.Entry(userCards).State = EntityState.Deleted;
		}

		public bool IsDuplicateEmail(string emailAddress)
		{
			var result = (from u in context.User
						  where u.EmailAddress == emailAddress
						  select u.EmailAddress
						  ).ToList();
			
			return result.Count > 0;

		}

		public bool AddToUserAccount(long userId,string account, double amount)
		{
            UserAccounts userAccounts = new UserAccounts
            {
                AccountNumber = account,
                AvailableBalance = 1000,
                UserId = userId,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            context.Add(userAccounts);
			return context.SaveChanges() > 0;

		}

		public bool ResetPassword(string password,long userId)
		{
			var result = context.User.Where(x => x.Id == userId).FirstOrDefault();
			result.Password = Constants.GetSecurePassword(password);
			context.Entry(result).State = EntityState.Modified;
			var data = context.SaveChanges();
			return data > 0;
		}

		public List<Users> GetAllListOfUsers()
		{
			return context.User.ToList();
		}
	}
}

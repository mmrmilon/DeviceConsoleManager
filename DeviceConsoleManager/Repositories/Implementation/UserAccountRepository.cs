using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Implementation
{
	public class UserAccountRepository : IUserAccountRepository
	{
		private DatabaseContext context;

		public UserAccountRepository(DatabaseContext context)
		{
			this.context = context;
		}


		public bool Delete(UserAccounts userAccounts)
		{
			var result = context.UserAccounts.Where(x => x.Id == userAccounts.Id)
				.FirstOrDefault();
			
			context.Entry(result).State = EntityState.Deleted;
			return context.SaveChanges() > 0;

		}

		public List<UserAccountDataModel> GetAll()
		{
			var result = (
				from uc in context.UserAccounts
				join u in context.User on uc.UserId equals u.Id
				select new UserAccountDataModel
				{
					Id = uc.Id,
					UserId = u.Id,
					UserName = u.UserName,
					AccountNumber = uc.AccountNumber,
					AvailableBalance = uc.AvailableBalance
				}).ToList();

			return result;
			
		}

		public List<Users> GetAllUsers()
		{
			var result =
			(
			from c in context.User
			join p in context.UserAccounts on c.Id equals p.UserId into ps
			from p in ps.DefaultIfEmpty()
			where p == null
			select new Users
			{
				Id = c.Id,
				UserName = c.UserName,
				MobileNumber = c.MobileNumber

			}).ToList();

			return result;
		}

		public long Save(UserAccounts userAccounts)
		{
            var aUserAccount = new UserAccounts
            {
                Guid = Guid.NewGuid().ToString().ToUpper(),
                UserId = userAccounts.UserId,
                AccountNumber = userAccounts.AccountNumber,
                AvailableBalance = userAccounts.AvailableBalance,
                CreatedOn = DateTime.Now,
                IsActive = true
            };
            context.Add(aUserAccount);

			context.SaveChanges();

			return aUserAccount.Id;
		}

		public bool Update(UserAccounts userAccounts)
		{
			var result = context.UserAccounts.Where(x => x.Id == userAccounts.Id)
				.FirstOrDefault();
			result.AvailableBalance = userAccounts.AvailableBalance;
			result.UpdatedOn = DateTime.Now;
			context.Entry(result).State = EntityState.Modified;
			return context.SaveChanges() > 0;
		}
	}
}

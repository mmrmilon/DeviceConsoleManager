using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PCSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Implementation
{
	public class UserCardRepository : IUserCardRepository
	{
		private DatabaseContext context;
		private object SCardScope;

		public UserCardRepository(DatabaseContext context)
		{
			this.context = context;
		}

		public List<Users> GetAllUsersNotHasCard()
		{

			var result =
			(
			from c in context.User
			join p in context.UserCards on c.Id equals p.UserId into ps
			from p in ps.DefaultIfEmpty()
			where p == null
			select new Users
			{
				Id = c.Id,
				UserName = c.UserName

			}).ToList();

			return result;
			
		}

		public List<CardUserDataModel> GetCardUsers()
		{
			var result = (
				from card in context.UserCards
				join user in context.User on card.UserId equals user.Id
				select new CardUserDataModel
				{
                    Card = card,
                    User = user,
					UserName = user.UserName,
					UserCardId = card.Id,
					ExpiryDate = card.ExpairDate,
					ChipCardNo = card.ChipCardNo.ToString()
				}).ToList();

			return result;
		}

		public bool WriteCardUsers(UserCards userCards)
		{
            UserCards userCard = new UserCards
            {
                Guid = Guid.NewGuid().ToString().ToUpper(),
                ChipCardNo = userCards.ChipCardNo,
                UserId = userCards.UserId,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                ExpairDate = userCards.ExpairDate,
                IsActive = true,
                IsBlocked = false
            };
            context.Add(userCard);
			context.SaveChanges();
			
			return UpdatePrefix();
		}

		private bool UpdatePrefix()
		{
			var result = this.GetPrifix();
			result.IsActive = true;
			context.Entry(result).State = EntityState.Modified;
			return context.SaveChanges() > 0;
		}

		private Prefix GetPrifix()
		{
			var result = (from prefix in context.Prefixes select prefix).FirstOrDefault();
			return result;
		}

        public bool Update(UserCards card)
        {
            var result = context.UserCards.Where(x => x.Id == card.Id).FirstOrDefault();
            result.ExpairDate = card.ExpairDate;
            result.IsActive = card.IsActive;

            context.Entry(result).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public bool Delete(long Id)
        {
            var user = context.UserCards.Where(x => x.Id == Id).FirstOrDefault();
            context.Entry(user).State = EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}

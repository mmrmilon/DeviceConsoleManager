using System;
using System.Collections.Generic;
using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeviceConsoleManager.Repositories.Implementation
{
    public class AccountRechargeRepository : IAccountRechargeRepository
    {
        private readonly DatabaseContext context;

        public AccountRechargeRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public List<AccountRechargeDataModel> GetAll()
        {
            var result = (from ar in context.AccountRecharge
                          join u in context.User on ar.UserId equals u.Id
                          join pm in context.PaymentMethod on ar.PaymentMethodId equals pm.Id
                          select new AccountRechargeDataModel
                          {
                              AccountRecharge = ar,
                              User = u,
                              PaymentMethod = pm
                          }).ToList();
            return result;
        }

        public AccountRecharge Save(AccountRecharge model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                var userAccount = context.UserAccounts.Where(x => x.UserId == model.UserId).FirstOrDefault();
                try
                {
                    //Add User Account Account Recharge History
                    model.Guid = Guid.NewGuid().ToString().ToUpper();
                    model.IsActive = true;
                    model.CreatedOn = DateTime.Now;
                    model.UpdatedOn = DateTime.Now;
                    context.Add(model);
                    context.SaveChanges();

                    //Update User Account Balance
                    userAccount.AvailableBalance = userAccount.AvailableBalance + model.RechargeAmount;
                    userAccount.UpdatedOn = DateTime.Now;
                    context.Entry(userAccount).State = EntityState.Modified;
                    context.SaveChanges();

                    // if all success - commit first step (second step was success completed)
                    transaction.Commit();

                    return model;
                }
                catch (Exception)
                {
                    // if we have error - rollback first step (second step not be able accepted)
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public AccountRecharge Update(AccountRecharge model)
        {
            try
            {
                var result = context.AccountRecharge.Where(x => x.Id == model.Id).FirstOrDefault();

                result.UserId = model.UserId;
                result.FromAccount = model.FromAccount;
                result.TransictionId = model.TransictionId;
                result.PaymentMethodId = model.PaymentMethodId;
                result.RechargeAmount = model.RechargeAmount;
                result.UpdatedOn = DateTime.Now;

                context.Entry(result).State = EntityState.Modified;
                context.SaveChanges();

                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        
        public bool Delete(long id)
        {
            try
            {
                var user = context.AccountRecharge.Where(x => x.Id == id).FirstOrDefault();
                context.Entry(user).State = EntityState.Deleted;
                var result = context.SaveChanges();
                return result > 0;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}

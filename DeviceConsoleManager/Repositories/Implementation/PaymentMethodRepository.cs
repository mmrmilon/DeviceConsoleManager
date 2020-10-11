using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Implementation
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly DatabaseContext context;

        public PaymentMethodRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public List<PaymentMethods> GetAll()
        {
            return context.PaymentMethod.Where(x => x.IsActive).ToList();
        }

        public PaymentMethods Save(PaymentMethods model)
        {
            try
            {                
                model.CreatedOn = DateTime.Now;
                model.UpdatedOn = DateTime.Now;

                context.Add(model);
                context.SaveChanges();

                return model;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public PaymentMethods Update(PaymentMethods model)
        {
            try
            {
                var result = context.PaymentMethod.Where(x => x.Id == model.Id).FirstOrDefault();
                result.MethodName = model.MethodName;
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
                var user = context.PaymentMethod.Where(x => x.Id == id).FirstOrDefault();
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

using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Implementation
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private DatabaseContext context;

        public ErrorLogRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public ErrorLog Insert(ErrorLog model)
        {
            model.CreatedOn = DateTime.Now;
            model.UpdatedOn = DateTime.Now;
            model.IsActive = true;

            context.Add(model);
            context.SaveChanges();

            return model;
        }
    }
}

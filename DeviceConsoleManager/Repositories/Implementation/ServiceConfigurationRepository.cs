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
    public class ServiceConfigurationRepository : IServiceConfigurationRepository
    {
        private readonly DatabaseContext context;

        public ServiceConfigurationRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<ServiceConfigurationDataModel> GetAll()
        {
            var result = (from d in context.DeviceInformations
                          join dl in context.DeviceLocation on d.DeviceLocationId equals dl.Id
                          join sc in context.UserServiceConfigurations on d.Id equals sc.DeviceId into tempServiceConfig
                          from sc in tempServiceConfig.DefaultIfEmpty()
                          select new ServiceConfigurationDataModel
                          {
                              DeviceInformation = d,
                              DeviceLocation = dl,
                              ServiceConfiguration = sc
                          }).ToList();
            return result;
        }

        public UserServiceConfigurations Update(UserServiceConfigurations model)
        {
            if (model.Id > 0)
            {
                var result = context.UserServiceConfigurations.Where(x => x.Id == model.Id).FirstOrDefault();
                result.UpdatedOn = DateTime.Now;
                result.Unit = model.Unit;
                result.UnitRate = model.UnitRate;

                context.Entry(result).State = EntityState.Modified;
                context.SaveChanges();

                return result;
            }
            else
            {
                model.CreatedOn = DateTime.Now;
                model.IsActive = true;

                context.Add(model);
                context.SaveChanges();

                return model;
            }
        }
    }
}

using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Implementation
{
    public class DeviceLocationRepository : IDeviceLocationRepository
    {
        private readonly DatabaseContext context;

        public DeviceLocationRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public bool Delete(long id)
        {
            try
            {
                var user = context.DeviceLocation.Where(x => x.Id == id).FirstOrDefault();
                context.Entry(user).State = EntityState.Deleted;
                var result = context.SaveChanges();
                return result > 0;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public List<DeviceLocations> GetAll()
        {
            return context.DeviceLocation.ToList();
        }

        public DeviceLocations Save(DeviceLocations model)
        {
            try
            {
                model.Guid = Guid.NewGuid().ToString().ToUpper();
                model.CreatedOn = DateTime.Now;
                model.UpdatedOn = DateTime.Now;

                context.Add(model);
                context.SaveChanges();

                return model;
            }
            catch (System.Exception) {
                throw;
            }
        }

        public DeviceLocations Update(DeviceLocations model)
        {
            try
            {
                var result = context.DeviceLocation.Where(x => x.Id == model.Id).FirstOrDefault();

                result.LocationName = model.LocationName;
                result.LocationLevel = model.LocationLevel;
                result.IsActive = model.IsActive;
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
    }
}

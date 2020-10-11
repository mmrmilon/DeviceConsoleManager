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
    public class UserServiceReportRepository : IUserServiceReportRepository
    {
        private readonly DatabaseContext context;

        public UserServiceReportRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<UserServiceDataModel> GetUserServiceReport(long customerId, long deviceId, DateTime? startDate, DateTime? endDate)
        {

            var query = (from p in context.UserServices
                         group p by p.StartTime into op
                         select new
                         {
                             StartTime = op.Key,
                             EndTime = op.Max(x => x.EndTime)
                         }).AsEnumerable();

            var queryResult = (from us in context.UserServices
                               join q in query on us.EndTime equals q.EndTime
                               select us).AsEnumerable();

            var result = (from u in context.User
                          join ur in context.UserRole on u.RoleId equals ur.Id
                          join us in queryResult on u.Id equals us.UserId
                          join trn in context.Transictions on us.Id equals trn.ServiceId
                          join d in context.DeviceInformations on us.DeviceId equals d.Id
                          join dl in context.DeviceLocation on d.DeviceLocationId equals dl.Id
                          select new UserServiceDataModel
                          {
                              User = u,
                              UserRole = ur,
                              UserService = us,
                              Transiction = trn,
                              DeviceInformation = d,
                              DeviceLocation = dl
                          }).AsEnumerable().Where(x => x.UserRole.RoleName.ToUpper().Equals(Constants.CUSTOMER.ToUpper()));

            if (customerId > 0)
                result = result.Where(x => x.User.Id == customerId);

            if (deviceId > 0)
                result = result.Where(x => x.DeviceInformation.Id == deviceId);

            if (startDate.HasValue)
                result = result.Where(x => x.UserService.CreatedOn >= startDate);

            if (endDate.HasValue)
                result = result.Where(x => x.UserService.CreatedOn <= endDate);

            return result.ToList();
        }

        public IEnumerable<UserServiceDataModel> GetDeviceServiceRecords()
        {
            var dateCriteria = DateTime.Now.Date.AddDays(-7);

            var query = (from p in context.UserServices
                         group p by p.StartTime into op
                         select new
                         {
                             StartTime = op.Key,
                             EndTime = op.Max(x => x.EndTime)
                         }).AsEnumerable();

            var queryResult = (from us in context.UserServices
                               join q in query on us.EndTime equals q.EndTime
                               where us.StartTime >= dateCriteria
                               select us).AsEnumerable();

            var result = (from us in queryResult
                          join trn in context.Transictions on us.Id equals trn.ServiceId
                          join d in context.DeviceInformations on us.DeviceId equals d.Id
                          select new UserServiceDataModel
                          {
                              UserService = us,
                              Transiction = trn,
                              DeviceInformation = d
                          }).AsEnumerable()
                          .OrderBy(x => x.DeviceInformation.DeviceName)
                          .ThenBy(x => x.UserService.StartTime);
           
            return result.ToList();
        }

        public IEnumerable<UserServiceDataModel> GetDeviceStatusReport()
        {
            var queryResult = (from us in context.DeviceStatus.Where(x=>x.IsActive)
                               select us).AsEnumerable();

            var result = (from d in context.DeviceInformations
                          join dl in context.DeviceLocation on d.DeviceLocationId equals dl.Id
                          join ds in queryResult on d.Id equals ds.DeviceId into tempDeviceStatus
                          from ds in tempDeviceStatus.DefaultIfEmpty()
                          join u in context.User on ds.UserId equals u.Id into tempUser
                          from u in tempUser.DefaultIfEmpty()
                          select new UserServiceDataModel
                          {
                              DeviceInformation = d,
                              DeviceLocation = dl,
                              DeviceStatus = ds,
                              User = u
                          }).AsEnumerable();
            
            return result.ToList();
        }

        public IEnumerable<DeviceServiceGraphDetailsDataModel> GetDeviceService(long customerId , long deviceId )
		{
            var result = (
                from t in context.Transictions
                join d in context.UserServices on t.ServiceId equals d.Id
                join di in context.DeviceInformations on d.DeviceId equals di.Id
                join u in context.User on d.UserId equals u.Id
                join ur in context.UserRole on u.RoleId equals ur.Id
                where (d.UserId == customerId || customerId == 0)
                && (d.DeviceId == deviceId || deviceId == 0)
                && ur.RoleName.ToUpper().Equals(Constants.CUSTOMER.ToUpper())
                group new { t, di } by di.Id into g
                select new DeviceServiceGraphDetailsDataModel
                {
                    DeviceId = g.FirstOrDefault().di.Id,
                    DeviceName = g.FirstOrDefault().di.DeviceName,
                    Amount = g.Sum(x => x.t.TransictionAmount)
                }).ToList();

			return result;

		}

		public IEnumerable<UserServiceGraphDetailsDataModel> GetUserService(long customerId, long deviceId)
		{
			var result = (
				from t in context.Transictions
				join d in context.UserServices on t.ServiceId equals d.Id
				join u in context.User on  d.UserId equals u.Id
				join ur in context.UserRole on u.RoleId equals ur.Id
				where (d.UserId == customerId || customerId == 0) 
					&& (d.DeviceId == deviceId || deviceId == 0)
					&& ur.RoleName.ToUpper().Equals(Constants.CUSTOMER.ToUpper())

			group new { t, u } by u.Id into g
				select new UserServiceGraphDetailsDataModel
				{
					UserId = g.FirstOrDefault().u.Id,
					UserName = g.FirstOrDefault().u.UserName,
					Amount = g.Sum(x => x.t.TransictionAmount)
				}
				).ToList();

			return result;

		}
	}
}

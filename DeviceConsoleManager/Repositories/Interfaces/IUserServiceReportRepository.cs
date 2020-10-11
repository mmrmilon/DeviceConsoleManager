using DeviceConsoleManager.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
    public interface IUserServiceReportRepository
    {
        IEnumerable<UserServiceDataModel> GetUserServiceReport(long customerId, long deviceId, DateTime? startDate, DateTime? endDate);

		IEnumerable<DeviceServiceGraphDetailsDataModel> GetDeviceService(long customerId, long deviceId);

		IEnumerable<UserServiceGraphDetailsDataModel> GetUserService(long customerId, long deviceId);

        IEnumerable<UserServiceDataModel> GetDeviceStatusReport();

        IEnumerable<UserServiceDataModel> GetDeviceServiceRecords();
    }
}

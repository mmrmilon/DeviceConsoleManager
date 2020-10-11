using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
	public interface IUserDeviceRepository
	{
		List<UserDeviceDataModel> GetAll();

        List<DeviceInformations> GetUserDeviceBy(long userId);

        bool Add(IEnumerable<UserGroups> model, long userId);

        bool Update(UserDeviceDataModel userdevice);

		bool Delete(long userId);

		List<Users> GetAllUsers();

        IEnumerable<UserDeviceMappingDataModel> GetUserDeviceMapping(long userId);

    }
}

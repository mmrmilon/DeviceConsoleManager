using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
	public interface IDeviceRepository
	{
		List<DeviceInformationsDataModel> GetAll();

		bool Save(DeviceInformations deviceInformations);

		bool Update(DeviceInformations deviceInformations);

		bool Delete(long deviceInfoId);

		UserReqeustStatusDataModel userChipCardValidation(string device_token, string chip_id);

		bool SeviceEnd(string chip_id, string device_token, string start_time, string end_time, double service_amount);

        bool ServiceEnd(string chip_id, string device_token, string start_time, string end_time, double service_amount);

        bool ServiceStart(string chip_id, string device_token, string start_time);

        UserServiceConfigurations GetUserServices();

		bool UpdateUserService(UserServiceConfigurations userServicesconfig);
	}
}

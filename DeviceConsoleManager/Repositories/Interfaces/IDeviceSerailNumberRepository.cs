using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
	public interface IDeviceSerailNumberRepository
	{
		List<DeviceSerialNumber> GetAll();

		string GenerateRandomNumber();

		string GetTopSerialNo();

		bool SetDeviceSerialNumber(string serial_no);
	}
}

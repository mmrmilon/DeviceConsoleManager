using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Interfaces
{
    public interface IDeviceLocationRepository
    {
        List<DeviceLocations> GetAll();

        DeviceLocations Save(DeviceLocations model);

        DeviceLocations Update(DeviceLocations model);

        bool Delete(long id);
    }
}

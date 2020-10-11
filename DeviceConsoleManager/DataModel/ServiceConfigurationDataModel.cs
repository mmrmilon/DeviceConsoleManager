using DeviceConsoleManager.Models;

namespace DeviceConsoleManager.DataModel
{
    public class ServiceConfigurationDataModel
    {
        public UserServiceConfigurations ServiceConfiguration { get; set; }

        public DeviceInformations DeviceInformation { get; set; }

        public DeviceLocations DeviceLocation { get; set; }
    }
}

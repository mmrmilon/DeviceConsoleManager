using DeviceConsoleManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.DataModel
{
    public class UserDeviceMappingDataModel
    {
        public DeviceInformations DeviceInformation { get; set; }

        public DeviceLocations DeviceLocation { get; set; }

        public UserGroups UserGroup { get; set; }

        public bool IsChecked { get; set; }

        public long UserId { get; set; }
    }
}

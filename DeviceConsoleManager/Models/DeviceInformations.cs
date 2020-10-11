using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{

    [Table("device_informaions")]
    public class DeviceInformations : Entity
    {
        [Column("guid")]
        public string DeviceGuid { get; set; }

        [Column("device_code")]
        public string DeviceCode { get; set; }

        [Column("device_name")]
        public string DeviceName { get; set; }

        [Column("device_location_id")]
        public long DeviceLocationId { get; set; }

        [Column("device_token")]
        public string DeivceToken { get; set; }

    }
}

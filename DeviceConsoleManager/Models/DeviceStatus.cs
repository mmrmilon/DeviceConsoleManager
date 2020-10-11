using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceConsoleManager.Models
{
    [Table("device_status")]
    public class DeviceStatus : Entity
    {
        [Column("device_id")]
        public long DeviceId { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }
    }
}

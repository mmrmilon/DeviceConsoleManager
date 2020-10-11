using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
    [Table("users")]
    public class Users : Entity
    {
        [Column("guid")]
        public string Guid { get; set; }

        [Column("first_name")]
        public string FristName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("name")]
        public string UserName { get; set; }

        [Column("mobile_no")]
        public string MobileNumber { get; set; }

        [Column("email")]
        public string EmailAddress { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("user_role_id")]
        public long RoleId { get; set; }
    }
}

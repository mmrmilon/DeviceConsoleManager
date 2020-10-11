using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Models
{
	public class DatabaseContext:DbContext
	{

		public DatabaseContext(DbContextOptions<DatabaseContext> options)
		: base(options)
		{

		}

		public DbSet<Users> User { get; set; }

		public DbSet<UserRoles> UserRole { get; set; }

		public DbSet<Prefix> Prefixes { get; set; }

		public DbSet<UserCards> UserCards { get; set; }

		public DbSet<DeviceInformations> DeviceInformations { get; set; }

		public DbSet<UserGroups> UserGroups { get; set; }

		public DbSet<UserAccounts> UserAccounts { get; set; }

		public DbSet<UserServiceConfigurations> UserServiceConfigurations { get; set; }

		public DbSet<Transictions> Transictions { get; set; }

		public DbSet<UserServices> UserServices { get; set; }

        public DbSet<DeviceStatus> DeviceStatus { get; set; }

        public DbSet<DeviceSerialNumber> DeviceSerialNumbers { get; set; }

        public DbSet<DeviceLocations> DeviceLocation { get; set; }

        public DbSet<AccountRecharge> AccountRecharge { get; set; }

        public DbSet<PaymentMethods> PaymentMethod { get; set; }

        public DbSet<ErrorLog> ErrorLog { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Utility
{
	public static class Constants
	{
		public static string ADMIN = "Admin";
		public static string SUPERADMIN = "SuperAdmin";
		public static string OPERATOR = "Operator";
		public static string CUSTOMER = "Customer";

		public static string GetSecurePassword(string inputPassword)
		{
			var encoder = new UTF8Encoding();
			var sha256hasher = new SHA256Managed();
			byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(inputPassword));
			//var d = Convert.ToBase64String(hashedDataBytes);
			return Convert.ToBase64String(hashedDataBytes);
		}
	}
}

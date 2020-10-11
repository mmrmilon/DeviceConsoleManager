using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Implementation
{
	public class DeviceSerailNumberRepository : IDeviceSerailNumberRepository
	{
		private DatabaseContext context;

		public DeviceSerailNumberRepository(DatabaseContext context)
		{
			this.context = context;
		}

		public string GenerateRandomNumber()
		{
			var message = "Save";
			var lastnumber = this.GetLastSerialNo();
			var length = 600;

			if (this.IsAllDevicesUsed())
			{
				for(int serialNo = lastnumber; serialNo < (lastnumber + length ); serialNo++)
				{
					var prefix = "";

					if (serialNo.ToString().Count() == 1)
					{
						prefix = "000";
					}
					else if (serialNo.ToString().Count() == 2)
					{
						prefix = "00";
					}
					else if (serialNo.ToString().Count() == 3)
					{
						prefix = "0";
					}
					else
					{
						prefix = "";
					}
                    DeviceSerialNumber deviceSerialNumber = new DeviceSerialNumber
                    {
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                        IsActive = true,
                        IsUsed = false,
                        SerialNumber = FourDigitGenerateRandomNo().ToString() + FourDigitGenerateRandomNo().ToString() + FourDigitGenerateRandomNo().ToString() + (prefix + serialNo).ToString()
                    };
                    context.DeviceSerialNumbers.Add(deviceSerialNumber);
					context.SaveChanges();

				}
			}
			else
			{
				message = "Please Use All Number";
			}


			return message;
		}

		public int GetLastSerialNo()
		{
			var serialNo = 1; 
			if(context.DeviceSerialNumbers.FirstOrDefault() == null)
			{
				return serialNo;
			}
			else
			{
				var result =
						Convert.ToInt32(
					 context.DeviceSerialNumbers
					.OrderByDescending(x => x.Id).FirstOrDefault().SerialNumber
					);

				return result;
			}
		
		}

		public int FourDigitGenerateRandomNo()
		{
			int _min = 1000;
			int _max = 9999;
			Random _rdm = new Random();
			return _rdm.Next(_min, _max);
		}

		public bool SetDeviceSerialNumber(string serial_no)
		{
			var data = context.DeviceSerialNumbers.Where(x => x.SerialNumber == serial_no)
				.FirstOrDefault();

			data.IsUsed = true;

			context.Entry(data).State = EntityState.Modified;
			return context.SaveChanges() > 0;
		}


		public List<DeviceSerialNumber> GetAll()
		{
			var result = context.DeviceSerialNumbers.ToList();
			return result;
		}

		private bool IsAllDevicesUsed()
		{
			var  result = context.DeviceSerialNumbers.Where(x => x.IsUsed == false);

			if (result.Count() <= 0 || context.DeviceSerialNumbers.FirstOrDefault() == null)
			{
				return true;
			}
			return false;
		}

		public string GetTopSerialNo()
		{
			return context.DeviceSerialNumbers.Where(x=>x.IsUsed == false).FirstOrDefault().SerialNumber;
		}
	}
}

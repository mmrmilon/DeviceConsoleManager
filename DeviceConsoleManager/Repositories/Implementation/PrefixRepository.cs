using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceConsoleManager.Repositories.Implementation
{
	public class PrefixRepository : IPrefixRepository
	{
		private readonly DatabaseContext context;

		public PrefixRepository(DatabaseContext context)
		{
			this.context = context;
		}

		public Prefix GetPrifix()
		{
			var result = context.Prefixes.FirstOrDefault();
			return result;
		}

		private Prefix SavePrefix()
		{
            Prefix aPrefix = new Prefix
            {
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                IsActive = true,
                PrefixLevel = "000",
                SerailNo = 1
            };
            context.Add(aPrefix);
			context.SaveChanges();
			return aPrefix;
		}

		public string GeneratePrefix()
		{
			var result = GetPrifix();

			if (result.IsActive == true)
			{
				if (result == null)
				{
					result = SavePrefix();
				}
				else
				{
					if (result.SerailNo.ToString().Count() == 1)
					{
						result.PrefixLevel = "000";
					}
					else if (result.SerailNo.ToString().Count() == 2)
					{
						result.PrefixLevel = "00";
					}
					else if (result.SerailNo.ToString().Count() == 3)
					{
						result.PrefixLevel = "0";
					}
					else
					{
						result.PrefixLevel = "";
					}
				}
				result.SerailNo = result.SerailNo + 1;
				result.IsActive = false;
				context.Entry(result).State = EntityState.Modified;
				context.SaveChanges();
			}

			return result.SerailNo.ToString().PadLeft(4,'0');
		}
		
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeviceConsoleManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {


		private readonly IUserRepository userReposityory;
		private readonly IHostingEnvironment hostingEnvironment;

		public UserApiController(IUserRepository userReposityory, IHostingEnvironment hostingEnvironment)
		{
			this.userReposityory = userReposityory;
			this.hostingEnvironment = hostingEnvironment;
		}

		[HttpGet]
		[Route("GetAll")]
		public ActionResult GetAll(string roleName)
		{
			try
			{
				var result = userReposityory.GetAll(roleName);
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

        [HttpGet]
        [Route("GetCustomerList")]
        public ActionResult GetAllCustomer()
        {
            try
            {
                var result = userReposityory.GetAllCustomer();
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpGet]
		[Route("GetAllListOfUsers")]
		public ActionResult GetAllListOfUsers()
		{
			try
			{
				var result = userReposityory.GetAllListOfUsers();
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("Save")]
		public ActionResult Save(Users users)
		{
			try
			{
				var isDuplicate = userReposityory.IsDuplicateEmail(users.EmailAddress);
				if (isDuplicate)
				{
					return Ok(new { success = false, errorMessage = "duplicate email address" });
				}

				var result = userReposityory.SaveUserAndAddAccount(users);
				return Ok(new { success = true, successMessage = "Saved Successfully" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("Update")]
		public ActionResult Update(Users users)
		{
			try
			{
				var result = userReposityory.Update(users);
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("Delete")]
		public ActionResult Delete(long userId)
		{
			try
			{
				var result = userReposityory.Delete(userId);
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}
		[HttpPost]
		[Route("ResetPassword")]
		public ActionResult ResetPassword(string password,long userId)
		{
			try
			{
				var result = userReposityory.ResetPassword(password, userId);
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}

	}
}
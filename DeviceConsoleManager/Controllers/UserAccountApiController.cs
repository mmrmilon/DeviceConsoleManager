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
    public class UserAccountApiController : ControllerBase
    {

		private readonly IUserAccountRepository userAccountRepository;
		private readonly IHostingEnvironment hostingEnvironment;

		public UserAccountApiController(IUserAccountRepository userAccountRepository, IHostingEnvironment hostingEnvironment)
		{
			this.userAccountRepository = userAccountRepository;
			this.hostingEnvironment = hostingEnvironment;
		}

		[HttpGet("GetAll")]
		public ActionResult GetAll()
		{
			try
			{
				var result = userAccountRepository.GetAll();
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}
		[HttpPost("Save")]
		public ActionResult Save(UserAccounts userAccounts)
		{
			try
			{
				var result = userAccountRepository.Save(userAccounts);
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}


		[HttpPost("Update")]
		public ActionResult Update(UserAccounts userAccounts)
		{
			try
			{
				var result = userAccountRepository.Update(userAccounts);
				return Ok(new { success = true, successMessage = "User Account updated successfully !" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}


		[HttpPost("Delete")]
		public ActionResult Delete(UserAccounts userAccounts)
		{
			try
			{
				var result = userAccountRepository.Delete(userAccounts);
				return Ok(new { success = true, successMessage = "user account deleted successfully" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}

		}
		[HttpGet("GetAllUsers")]
		public ActionResult GetAllUsers()
		{
			try
			{
				var result = userAccountRepository.GetAllUsers();
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

	}
}
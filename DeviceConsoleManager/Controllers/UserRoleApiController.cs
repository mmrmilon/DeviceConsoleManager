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
    public class UserRoleApiController : ControllerBase
    {

		private readonly IUserRoleRepository userRoleRepository;
		private readonly IHostingEnvironment hostingEnvironment;

		public UserRoleApiController(IUserRoleRepository userRoleRepository, IHostingEnvironment hostingEnvironment)
		{
			this.userRoleRepository = userRoleRepository;
			this.hostingEnvironment = hostingEnvironment;
		}

		[HttpGet]
		[Route("GetAll")]
		public ActionResult GetAll()
		{

			try
			{
				var result = userRoleRepository.GetAll();
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpGet]
		[Route("GetAllByRoleName")]
		public ActionResult GetAll(string roleName)
		{

			try
			{
				var result = userRoleRepository.GetAll(roleName);
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("Save")]
		public ActionResult Save(UserRoles userRoles)
		{

			try
			{
				var result = userRoleRepository.Add(userRoles);
				return Ok(new { success = true, successMessage = "Saved Successfully" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("Update")]
		public ActionResult Update(UserRoles userRoles)
		{

			try
			{
				var result = userRoleRepository.Update(userRoles);
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
				var result = userRoleRepository.Delete(userId);
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}
	}
}
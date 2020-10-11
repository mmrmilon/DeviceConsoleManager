using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Models;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeviceConsoleManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDeviceApiController : ControllerBase
    {
		private readonly IUserDeviceRepository userDeviceRepository;
		private readonly IHostingEnvironment hostingEnvironment;

		public UserDeviceApiController(IUserDeviceRepository userDeviceRepository, IHostingEnvironment hostingEnvironment)
		{
			this.userDeviceRepository = userDeviceRepository;
			this.hostingEnvironment = hostingEnvironment;
		}

		[HttpGet]
		[Route("GetAll")]
		public ActionResult GetAll()
		{

			try
			{
				var data = userDeviceRepository.GetAll();
				return Ok(new { success = true, data = data });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

        [HttpGet]
        [Route("GetUserDeviceBy")]
        public ActionResult GetUserDeviceBy(long userId)
        {

            try
            {
                var result = userDeviceRepository.GetUserDeviceBy(userId);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpGet]
        [Route("GetUserDeviceMappingBy")]
        public ActionResult GetUserDeviceMappingBy(long userId)
        {

            try
            {
                var result = userDeviceRepository.GetUserDeviceMapping(userId);

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpPost]
        [Route("SaveDeviceMaping")]
        public ActionResult SaveDeviceMaping(IEnumerable<UserGroups> data, long userId)
        {
            try
            {
                var result = userDeviceRepository.Add(data, userId);
                if(result)
                    return Ok(new { success = true, successMessage = "User Device Mapped Successfully!" });
                else
                    return Ok(new { success = false, successMessage = "User Device Mapped Failed!" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpPost]
		[Route("Save")]
		public ActionResult Save(UserDeviceDataModel userDeviceDataModel)
		{

			try
			{
				//var data = userDeviceRepository.Add(userDeviceDataModel);
				return Ok(new { success = true, successMessage = "User Group Added Successfully!" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpGet]
		[Route("GetAllUsers")]
		public ActionResult GetAllUsers()
		{
			try
			{
				var data = userDeviceRepository.GetAllUsers();
				return Ok(new { success = true, data = data });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("Delete")]
		public ActionResult Delete(long pUserDeviceId)
		{

			try
			{
				var data = userDeviceRepository.Delete(pUserDeviceId);
				return Ok(new { success = true, successMessage = "Deleted Successfully!" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("Update")]
		public ActionResult Update(UserDeviceDataModel userDeviceDataModel)
		{
			try
			{
				var data = userDeviceRepository.Update(userDeviceDataModel);
				return Ok(new { success = true, successMessage = "Updated Successfully!" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}
	}
}
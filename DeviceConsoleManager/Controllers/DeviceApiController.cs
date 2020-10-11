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
    public class DeviceApiController : ControllerBase
    {
		private readonly IDeviceRepository deviceRepository;
		private readonly IHostingEnvironment hostingEnvironment;

		public DeviceApiController(IDeviceRepository deviceRepository, IHostingEnvironment hostingEnvironment)
		{
			this.deviceRepository = deviceRepository;
			this.hostingEnvironment = hostingEnvironment;
		}
		
		[HttpGet]
		[Route("GetAll")]
		public ActionResult GetAll()
		{

			try
			{
				var data = deviceRepository.GetAll();
				return Ok(new { success = true, data = data });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("Update")]
		public ActionResult Update(DeviceInformations deviceInformation)
		{

			try
			{
				var data = deviceRepository.Update(deviceInformation);
				return Ok(new { success = true, successMessage = "Updated Successfully!" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("Save")]
		public ActionResult Save(DeviceInformations deviceInformations)
		{

			try
			{
				var data = deviceRepository.Save(deviceInformations);

				return Ok(new { success = true, successMessage = "Device Saved Successfully!" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("Delete")]
		public ActionResult Delete(long pDeviceId)
		{
			try
			{
				var data = deviceRepository.Delete(pDeviceId);
				return Ok(new { success = true, succssMessage = "Device Deleted Successfully!" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpGet]
		[Route("SeviceEnd")]
		public ActionResult SeviceEnd(string chip_id, string device_token, string start_time, string end_time, double service_amount)
		{
			try
			{
				var data = deviceRepository.SeviceEnd(chip_id,device_token,start_time,end_time,service_amount);
                if(data)
                    return Ok(new { success = true, succssMessage = "Service Completed!" });
                else
                    return Ok(new { success = false, errorMessage = "Invalid Device Token or Card Number!" });
            }
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

        [HttpGet]
        [Route("ServiceEnd")]
        public ActionResult ServiceEnd(string chip_id, string device_token, string start_time, string end_time, double service_amount)
        {
            try
            {
                var data = deviceRepository.ServiceEnd(chip_id, device_token, start_time, end_time, service_amount);
                if (data)
                    return Ok(new { success = true, succssMessage = "Service Completed!" });
                else
                    return Ok(new { success = false, errorMessage = "Invalid Device Token or Card Number!" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpGet]
        [Route("ServiceStart")]
        public ActionResult ServiceStart(string chip_id, string device_token, string start_time)
        {
            try
            {
                var data = deviceRepository.ServiceStart(chip_id, device_token, start_time);
                if (data)
                    return Ok(new { success = true, succssMessage = "Service Started" });
                else
                    return Ok(new { success = false, errorMessage = "Failed to Start Service" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpGet]
		[Route("userChipCardValidation")]
		public ActionResult UserChipCardValidation(string chip_id, string device_token)
		{
			try
			{
				var data = deviceRepository.userChipCardValidation(device_token, chip_id);
				return Ok(data);
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}
	}
}
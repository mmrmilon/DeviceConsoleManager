using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeviceConsoleManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceSerialNumberApiController : ControllerBase
    {
		private readonly IDeviceSerailNumberRepository deviceSerailNumberRepository;
		private readonly IHostingEnvironment hostingEnvironment;

		public DeviceSerialNumberApiController(IDeviceSerailNumberRepository deviceSerailNumberRepository, IHostingEnvironment hostingEnvironment)
		{
			this.deviceSerailNumberRepository = deviceSerailNumberRepository;
			this.hostingEnvironment = hostingEnvironment;
		}


		[HttpGet]
		[Route("GetAll")]
		public ActionResult GetAll()
		{

			try
			{
				var result = deviceSerailNumberRepository.GetAll();
				return Ok(new { success = true, data = result });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpGet]
		[Route("GetTopSerialNo")]
		public ActionResult GetTopSerialNo()
		{
			try
			{
				var data = deviceSerailNumberRepository.GetTopSerialNo();
				return Ok(new { success = true, data = data });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpPost]
		[Route("GenerateRandomNumber")]
		public ActionResult GenerateRandomNumber()
		{
			try
			{
				var data = deviceSerailNumberRepository.GenerateRandomNumber();
				return Ok(new { success = true, successMessage = "Device Number Generated Successfully!" });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

	}
}
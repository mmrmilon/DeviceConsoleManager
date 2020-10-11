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
    public class UserServiceReportApiController : ControllerBase
    {
        private readonly IUserServiceReportRepository repository;
        private readonly IHostingEnvironment hostingEnvironment;

        public UserServiceReportApiController(IUserServiceReportRepository repository, IHostingEnvironment hostingEnvironment)
        {
            this.repository = repository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("GetUserServiceDetailsBy")]
        public ActionResult GetUserServiceReport(long customerId, long deviceId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var response = repository.GetUserServiceReport(customerId, deviceId, startDate, endDate);

                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpGet]
        [Route("GetDeviceServiceRecords")]
        public ActionResult GetDeviceServiceRecords()
        {
            try
            {
                var response = repository.GetDeviceServiceRecords();

                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpGet]
		[Route("GetUserServiceDetailsByUserId")]
		public ActionResult GetUserServiceDetailsByUserId(long customerId,long deviceId, DateTime? startDate, DateTime? endDate)
		{
			try
			{
				var response = repository.GetUserService(customerId, deviceId);
				return Ok(new { success = true, data = response });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		[HttpGet]
		[Route("GetDeviceServiceDetailsByDeviceId")]
		public ActionResult GetDeviceServiceDetailsByDeviceId(long customerId, long deviceId, DateTime? startDate, DateTime? endDate)
		{
			try
			{
				var response = repository.GetDeviceService(customerId, deviceId);
				return Ok(new { success = true, data = response });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

        [HttpGet]
        [Route("ExportUserServiceDetailsBy")]
        public ActionResult ExportUserServiceDetailsBy(long customerId, long deviceId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var response = repository.GetUserServiceReport(customerId, deviceId, startDate, endDate);

                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpGet]
        [Route("GetDeviceServiceStatus")]
        public ActionResult GetDeviceServiceStatus()
        {
            try
            {
                var response = repository.GetDeviceStatusReport();

                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        //public JsonResult ExportDataOfTrialBalance()
        //{
        //    try
        //    {
        //        trialBalanceService.ExportDataOfTrialBalance().GetExportCsv(Constants.ExportedTrialBalance);

        //        return Json(new { success = true, errorMessage = "OK" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = true, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

    }
}
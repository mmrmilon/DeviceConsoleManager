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
    public class DeviceLocationApiController : ControllerBase
    {
        private readonly IDeviceLocationRepository repository;
        private readonly IHostingEnvironment hostingEnvironment;

        public DeviceLocationApiController(IDeviceLocationRepository repository, IHostingEnvironment hostingEnvironment)
        {
            this.repository = repository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            try
            {
                var response = repository.GetAll();

                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpPost]
        [Route("Save")]
        public ActionResult Save(DeviceLocations model)
        {

            try
            {
                var data = repository.Save(model);

                return Ok(new { success = true, successMessage = "Saved Successfully!" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }


        [HttpPost]
        [Route("Update")]
        public ActionResult Update(DeviceLocations model)
        {

            try
            {
                var data = repository.Update(model);

                return Ok(new { success = true, successMessage = "Updated Successfully!" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                var data = repository.Delete(id);

                return Ok(new { success = true, succssMessage = "Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }
    }
}
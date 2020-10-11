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
    public class ServiceConfigurationApiController : ControllerBase
    {
        private readonly IServiceConfigurationRepository serviceConfigurationRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public ServiceConfigurationApiController(IServiceConfigurationRepository serviceConfigurationRepository, IHostingEnvironment hostingEnvironment)
        {
            this.serviceConfigurationRepository = serviceConfigurationRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        
        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            try
            {
                var result = serviceConfigurationRepository.GetAll();

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }

        [HttpPost]
        [Route("UpdateServiceConfiguration")]
        public ActionResult UpdateServiceConfiguration(UserServiceConfigurations model)
        {
            try
            {
                var data = serviceConfigurationRepository.Update(model);

                return Ok(new { success = true, successMessage = "Configuration Updated Successfully." });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, errorMessage = ex.GetBaseException() });
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Repositories.Implementation;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DeviceConsoleManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInApiController : ControllerBase
    {
		
		private readonly IUserRepository userReposityory;
		private readonly IHostingEnvironment hostingEnvironment;
		private IConfiguration _config;

		public LogInApiController(IUserRepository userReposityory, IHostingEnvironment hostingEnvironment)
		{
			this.userReposityory = userReposityory;
			this.hostingEnvironment = hostingEnvironment;
		}

		[HttpPost]
		[Route("Login")]
		public IActionResult Login(string email, string password)
		{
			try
			{
				IActionResult response = Unauthorized();
				var user = userReposityory.Login(email, password);
				var tokenString = "";
				if (user != null)
				{
					tokenString = GenerateJSONWebToken(user);
					response = Ok(new { token = tokenString , sessionUser  = user });
				}
				else
				{
					return Ok(new { success = false, errorMessage = "User Name or Password Invalid" });
				}

				return Ok(new { success = true, sessionUser = user, token= tokenString });
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			} 
		}

		[HttpGet]
		[Route("GetAuthenticUser")]
		public IActionResult GetAuthenticUser(string email, string password)
		{
			try
			{
				var user = userReposityory.GetAuthenticUser(email, password);
				if (user != null)
				{
					return Ok(user);
				}
				else
				{
					return Ok(new { success = false, errorMessage = "User Name or Password Invalid" });
				}
				
			}
			catch (Exception ex)
			{
				return Ok(new { success = false, errorMessage = ex.GetBaseException() });
			}
		}

		private string GenerateJSONWebToken(SessionDataModel userInfo)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisismySecretKey"));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken("Test.com",
			  "Test.com",
			  null,
			  expires: DateTime.Now.AddMinutes(120),
			  signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		//Home page

	}
}
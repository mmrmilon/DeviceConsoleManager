﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DeviceConsoleManager.DataModel;
using DeviceConsoleManager.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DeviceConsoleManager.Controllers
{
    public class TokenController : Controller
    {

		private IConfiguration _config;
		private IUserRepository userReposityory;

		public TokenController(IConfiguration config, IUserRepository userReposityory)
		{
			_config = config;
			this.userReposityory = userReposityory;
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Login(string username , string password)
		{
			IActionResult response = Unauthorized();
			var user = this.userReposityory.Login(username, password);

			if (user != null)
			{
				var tokenString = GenerateJSONWebToken(user);
				response = Ok(new { token = tokenString });
			}

			return response;
		}

		private string GenerateJSONWebToken(SessionDataModel userInfo)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(_config["Jwt:Issuer"],
			  _config["Jwt:Issuer"],
			  null,
			  expires: DateTime.Now.AddMinutes(120),
			  signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		





	}
}
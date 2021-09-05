﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RevXApi.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Controllers
{
	public class TokenController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;

		public TokenController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		[Route("/token")]
		[HttpPost]
		public async Task<IActionResult> Create(string username, string password)
		{
			if (await IsValidUsernameAndPassword(username, password))
			{
				return new ObjectResult(await GenerateToken(username));
			}
			else
			{
				return BadRequest();
			}
		}

		private async Task<bool> IsValidUsernameAndPassword(string username, string password)
		{
			Console.WriteLine(await _userManager.FindByEmailAsync(username));
			IdentityUser user = await _userManager.FindByEmailAsync(username);
			return await _userManager.CheckPasswordAsync(user, password);
		}

		private async Task<dynamic> GenerateToken(string username)
		{
			var user = await _userManager.FindByEmailAsync(username);
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, username),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
				new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(7)).ToUnixTimeSeconds().ToString())
			};

			var token = new JwtSecurityToken(
				new JwtHeader(
					new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySECreTKeYIsSEcReTSoDONtTeLl")),
					SecurityAlgorithms.HmacSha256)),
				new JwtPayload(claims));

			var output = new
			{
				Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
				UserName = username
			};

			return output;
		}
	}
}

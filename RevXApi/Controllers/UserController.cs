using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RevXApi.Data;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UserController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IUserData _userData;
		private readonly ILogger<UserController> _logger;

		public UserController(ApplicationDbContext context,
			UserManager<IdentityUser> userManager,
			IUserData userData, ILogger<UserController> logger)
		{
			_context = context;
			_userManager = userManager;
			_userData = userData;
			_logger = logger;
		}

		[HttpGet]
		public UserModel GetById()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			return _userData.GetUserById(userId).First();
		}

		public record UserRegistrationModel(string FirstName, string LastName, string EmailAddress, string Password);

		[HttpPost]
		[Route("Register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register(UserRegistrationModel user)
		{
			if (ModelState.IsValid)
			{
				IdentityUser ExistingUser = await _userManager.FindByEmailAsync(user.EmailAddress);
				if (ExistingUser == null)
				{
					IdentityUser newUser = new()
					{
						Email = user.EmailAddress,
						EmailConfirmed = true,
						UserName = user.EmailAddress
					};
					IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
					if (result.Succeeded)
					{
						ExistingUser = await _userManager.FindByEmailAsync(user.EmailAddress);
						if (ExistingUser == null)
						{
							return BadRequest();
						}
						UserModel u = new()
						{
							Id = ExistingUser.Id,
							FirstName = user.FirstName,
							LastName = user.LastName,
							EmailAddress = user.EmailAddress
						};
						_userData.CreateUser(u);
						return Ok();
					}
				}
			}
			return BadRequest();
		}


		[Authorize]
		[HttpGet]
		[Route("Admin/GetAllUsers")]
		public List<ApplicationUserModel> GetAllUsers()
		{
			List<ApplicationUserModel> output = new List<ApplicationUserModel>();

			var users = _context.Users.ToList();
			var userRoles = from ur in _context.UserRoles
							join r in _context.Roles on ur.RoleId equals r.Id
							select new { ur.UserId, ur.RoleId, r.Name };

			foreach (var user in users)
			{
				ApplicationUserModel u = new ApplicationUserModel
				{
					Id = user.Id,
					Email = user.Email
				};

				u.Roles = userRoles.Where(x => x.UserId == u.Id).ToDictionary(x => x.RoleId, x => x.Name);

				output.Add(u);
			}

			return output;
		}

		[Authorize]
		[HttpGet]
		[Route("Admin/GetAllRoles")]
		public Dictionary<string, string> GetAllRoles()
		{

			var roles = _context.Roles.ToDictionary(x => x.Id, x => x.Name);
			return roles;
		}
	}
}

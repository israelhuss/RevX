using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RevXApi.Library.DataAccess;
using System;
using System.Security.Claims;
using System.Text.Json;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TestController : ControllerBase
	{
		private readonly IPlayground _playGround;
		private readonly ILogger<Playground> _logger;

		public TestController(IPlayground playGround, ILogger<Playground> logger)
		{
			_playGround = playGround;
			_logger = logger;
		}
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Get()
		{
			try
			{
				_logger.LogInformation("Hello From the test controller");
				_logger.LogDebug("Peekoboo");
				var res = _playGround.Play(User.FindFirstValue(ClaimTypes.NameIdentifier));
				return Ok(new string[] { "hi", "there", res });
			}
			catch (Exception ex)
			{
				return BadRequest(JsonSerializer.Serialize(ex.StackTrace));
			}
		}
	}
}

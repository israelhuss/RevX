using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly IPlayground _playGround;

		public TestController(IPlayground playGround)
		{
			_playGround = playGround;
		}
		[HttpGet]
		public IActionResult Get()
		{
			_playGround.Play();
			return Ok(new string[] { "hi", "there" });
		}
	}
}

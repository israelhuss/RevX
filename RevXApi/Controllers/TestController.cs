using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RevXApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(new string[] { "hi", "there" });
		}
	}
}

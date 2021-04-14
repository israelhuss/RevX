using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LogoController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			var path = System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).ToString(), @"Logo\Logo-text.jpg"));

			if (System.IO.File.Exists(path))
			{
				return File(path, "image/jpeg");
			}
			else
			{
				return StatusCode(404);
			}
		}
	}
}

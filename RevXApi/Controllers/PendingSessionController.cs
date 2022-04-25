using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class PendingSessionController : ControllerBase
	{
		private readonly IPendingSessionData _pendingSessionData;
		private readonly ILogger<PendingSessionController> _logger;

		public PendingSessionController(IPendingSessionData pendingSessionData, ILogger<PendingSessionController> logger)
		{
			_pendingSessionData = pendingSessionData;
			_logger = logger;
		}

		[HttpGet]
		public List<SessionModel> GetAllSessions(int month, int year)
		{
			_logger.LogInformation($"User {User.FindFirstValue(ClaimTypes.NameIdentifier)} Asked for pending sessions in {month} {year}");
			return _pendingSessionData.GetByMonth(month, year, User.FindFirstValue(ClaimTypes.NameIdentifier));
		}

		[HttpPost]
		[AllowAnonymous]
		public IActionResult AddSchedule(ScheduleModel schedule)
		{
			try
			{
				_pendingSessionData.AddSchedule(schedule, User.FindFirstValue(ClaimTypes.NameIdentifier));
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("Delete/{id}")]
		public void Delete(int id)
		{
			_pendingSessionData.Delete(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
		}
	}
}

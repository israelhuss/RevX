using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class SessionController : ControllerBase
	{
		private readonly ISessionData _sessionData;

		public SessionController(ISessionData sessionData)
		{
			_sessionData = sessionData;
		}

		[HttpGet]
		public List<SessionModel> GetAllSessions()
		{
			return _sessionData.GetAllSessions(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}

		// FIX this method - get is not supposed to have body
		[HttpPost("GetByBillingStatus")]
		public List<SessionModel> GetByBillingStatus(BillingStatusModel billingStatus)
		{
			return _sessionData.GetByBillingStatus(billingStatus);
		}

		[HttpPost]
		public IActionResult SaveSession(SessionModel model)
		{
			model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			int affected = _sessionData.SaveSession(model);
			if (affected > 0)
			{
				return Created("/Session", model);
			}
			else
			{
				return StatusCode(406, "There was already a session for this time");
			}
		}

		[HttpPost]
		[Route("Edit")]
		public void EditSession(SessionModel model)
		{
			model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_sessionData.EditSession(model);
		}

		[HttpPost]
		[Route("Delete/{id}")]
		public void DeleteSession(int id)
		{
			_sessionData.DeleteSession(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
		}
	}
}

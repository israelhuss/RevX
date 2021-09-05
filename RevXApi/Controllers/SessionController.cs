using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System.Collections.Generic;

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
		public List<SessionModel> GetAllSessions([FromQuery] string userId)
		{
			return _sessionData.GetAllSessions(userId);
		}

		// FIX this method - get is not supposed to have body
		[HttpPost("GetByBillingStatus")]
		public List<SessionModel> GetByBillingStatus(BillingStatusModel billingStatus)
		{
			return _sessionData.GetByBillingStatus(billingStatus);
		}

		[HttpPost]
		public void SaveSession(SessionModel model)
		{
			_sessionData.SaveSession(model);
		}

		[HttpPost]
		[Route("Edit")]
		public void EditSession(SessionModel model)
		{
			_sessionData.EditSession(model);
		}

		[HttpPost]
		[Route("Delete/{id}")]
		public void DeleteSession(int id, [FromQuery] string userId)
		{
			_sessionData.DeleteSession(id, userId);
		}
	}
}

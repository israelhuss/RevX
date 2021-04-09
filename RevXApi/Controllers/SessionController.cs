using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
			return _sessionData.GetAllSessions();
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
		public void DeleteSession(int id)
		{
			_sessionData.DeleteSession(id);
		}
	}
}

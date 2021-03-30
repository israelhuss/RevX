using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
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

	}
}

using RevXApi.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface ISessionData
	{
		List<SessionModel> GetAllSessions();
	}
}
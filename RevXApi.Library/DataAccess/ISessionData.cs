using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface ISessionData
	{
		List<SessionModel> GetAllSessions();
		void SaveSession(SessionModel model);
		void EditSession(SessionModel model);
		void DeleteSession(int id);
	}
}
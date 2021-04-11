using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface ISessionData
	{
		List<SessionModel> GetAllSessions();
		List<SessionModel> GetByBillingStatus(BillingStatusModel billingStatus);
		void SaveSession(SessionModel model);
		void EditSession(SessionModel model);
		void DeleteSession(int id);
	}
}
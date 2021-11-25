using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface ISessionData
	{
		List<SessionModel> GetAllSessions(string userId);
		SessionModel GetById(int id, string userId);
		List<SessionModel> GetByBillingStatus(BillingStatusModel billingStatus);
		int SaveSession(SessionModel model);
		void EditSession(SessionModel model);
		void DeleteSession(int id, string userId);
	}
}
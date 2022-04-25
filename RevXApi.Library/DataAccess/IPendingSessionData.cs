using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IPendingSessionData
	{
		List<SessionModel> GetByMonth(int month, int year, string userId);
		void AddSchedule(ScheduleModel schedule, string userId);
		void Delete(int id, string userId);
	}
}
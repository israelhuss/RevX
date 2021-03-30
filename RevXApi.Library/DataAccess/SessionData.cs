using RevXApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class SessionData : ISessionData
	{
		private readonly ISqlDataAccess _sql;

		public SessionData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<SessionModel> GetAllSessions()
		{
			List<SessionModel> sessions = _sql.LoadData<SessionModel>("dbo.spSession_GetAll", "RevXData");
			return sessions;
		}

		public void AddSession(SessionModel model)
		{

		}
	}
}

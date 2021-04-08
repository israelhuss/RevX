using RevXApi.Library.Models;
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
		private readonly IStudentData _studentData;
		private readonly IProviderData _providerData;
		private readonly IBillingStatusData _billingStatusData;

		public SessionData(ISqlDataAccess sql, IStudentData studentData, IProviderData providerData, IBillingStatusData billingStatusData)
		{
			_sql = sql;
			_studentData = studentData;
			_providerData = providerData;
			_billingStatusData = billingStatusData;
		}

		public List<SessionModel> GetAllSessions()
		{
			List<SessionDbModel> sessions = _sql.LoadData<SessionDbModel>("dbo.spSession_GetAll", "RevXData");

			List<SessionModel> output = new();

			foreach (var session in sessions)
			{
				SessionModel model = new()
				{ 
					Id = session.Id,
					Date = session.Date,
					StartTime = session.StartTime,
					EndTime = session.EndTime,
					Notes = session.Notes
				};

				model.Student = _studentData.GetById(session.StudentId);
				model.Provider = _providerData.GetById(session.ProviderId);
				model.BillingStatus = _billingStatusData.GetById(session.BillingStatusId);


				output.Add(model);
			}

			return output;
		}

		public void SaveSession(SessionModel model)
		{
			var dbModel = new SessionDbModel()
			{
				StudentId = model.Student.Id,
				Date = model.Date,
				StartTime = model.StartTime,
				EndTime = model.EndTime,
				ProviderId = model.Provider.Id,
				BillingStatusId = model.BillingStatus.Id,
				Notes = model.Notes
			};

			_sql.SaveData("dbo.spSession_Insert", dbModel, "RevXData");
		}

		private TimeSpan ConvertToTimeSpan(string timeString)
		{
			var split = timeString.Split(':');
			int.TryParse(split[0], out int hours);
			int.TryParse(split[1], out int minutes);
			var output = new TimeSpan(hours, minutes, 00);
			return output;
		}
	}
}

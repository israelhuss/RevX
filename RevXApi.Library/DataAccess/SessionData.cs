using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

		public List<SessionModel> GetAllSessions(string userId)
		{
			List<SessionDbModel> sessions = _sql.LoadData<SessionDbModel, dynamic>("dbo.spSession_GetAll", new { userId }, "RevXData");

			List<SessionModel> output = new();

			foreach (var session in sessions)
			{
				SessionModel model = new()
				{
					Id = session.Id,
					UserId = session.UserId,
					Date = session.Date,
					StartTime = session.StartTime.ToString(),
					EndTime = session.EndTime.ToString(),
					Notes = session.Notes
				};

				model.Student = _studentData.GetById(session.StudentId, session.UserId);
				model.Provider = _providerData.GetById(session.ProviderId, session.UserId);
				model.BillingStatus = _billingStatusData.GetById(session.BillingStatusId);


				output.Add(model);
			}

			return output;
		}

		public SessionModel GetById(int id, string userId)
		{
			SessionDbModel session = _sql.LoadData<SessionDbModel, dynamic>("dbo.spSession_GetById", new { Id = id, userId }, "RevXData").FirstOrDefault();

			SessionModel output = new()
			{
				Id = session.Id,
				UserId = session.UserId,
				Date = session.Date,
				StartTime = session.StartTime.ToString(),
				EndTime = session.EndTime.ToString(),
				Notes = session.Notes
			};

			output.Student = _studentData.GetById(session.StudentId, session.UserId);
			output.Provider = _providerData.GetById(session.ProviderId, session.UserId);
			output.BillingStatus = _billingStatusData.GetById(session.BillingStatusId);

			return output;
		}


		// FIX this method - get is not supposed to have body
		public List<SessionModel> GetByBillingStatus(BillingStatusModel billingStatus)
		{
			var sessions = _sql.LoadData<SessionDbModel, dynamic>("dbo.spSession_GetByBillingStatus", new { BillingStatusId = billingStatus.Id }, "RevXData");

			List<SessionModel> output = new();

			foreach (var session in sessions)
			{
				SessionModel model = new()
				{
					Id = session.Id,
					UserId = session.UserId,
					Date = session.Date,
					StartTime = session.StartTime.ToString(),
					EndTime = session.EndTime.ToString(),
					Notes = session.Notes
				};

				model.Student = _studentData.GetById(session.StudentId, session.UserId);
				model.Provider = _providerData.GetById(session.ProviderId, session.UserId);
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
				UserId = model.UserId,
				Date = model.Date,
				StartTime = ConvertToTimeSpan(model.StartTime),
				EndTime = ConvertToTimeSpan(model.EndTime),
				ProviderId = model.Provider.Id,
				BillingStatusId = model.BillingStatus.Id,
				Notes = model.Notes
			};

			_sql.SaveData("dbo.spSession_Insert", dbModel, "RevXData");
		}

		public void EditSession(SessionModel model)
		{
			var dbModel = new SessionDbModel()
			{
				Id = model.Id,
				UserId = model.UserId,
				StudentId = model.Student.Id,
				Date = model.Date,
				StartTime = ConvertToTimeSpan(model.StartTime),
				EndTime = ConvertToTimeSpan(model.EndTime),
				ProviderId = model.Provider.Id,
				BillingStatusId = model.BillingStatus.Id,
				Notes = model.Notes
			};

			_sql.SaveData("dbo.spSession_Edit", dbModel, "RevXData");
		}

		public void DeleteSession(int id, string userId)
		{
			_sql.SaveData("dbo.spSession_Delete", new { Id = id, userId }, "RevXData");
		}

		private TimeSpan ConvertToTimeSpan(string timeString)
		{
			var split = timeString.Split(':');
			int.TryParse(split[ 0 ], out int hours);
			int.TryParse(split[ 1 ], out int minutes);
			var output = new TimeSpan(hours, minutes, 00);
			return output;
		}
	}
}

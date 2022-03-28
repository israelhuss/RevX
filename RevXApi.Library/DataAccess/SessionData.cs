using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RevXApi.Library.DataAccess
{
	public class SessionData : ISessionData
	{
		private readonly ISqlDataAccess _sql;
		private readonly IClientData _clientData;
		private readonly IProviderData _providerData;
		private readonly IBillingStatusData _billingStatusData;
		private readonly IHourlyRateData _rateData;

		public SessionData(ISqlDataAccess sql, IClientData clientData, IProviderData providerData, IBillingStatusData billingStatusData, IHourlyRateData rateData)
		{
			_sql = sql;
			_clientData = clientData;
			_providerData = providerData;
			_billingStatusData = billingStatusData;
			_rateData = rateData;
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
					Notes = session.Notes,
					InvoiceId = session.InvoiceId
				};

				model.Client = _clientData.GetById(session.ClientId, session.UserId);
				model.Provider = _providerData.GetById(session.ProviderId, session.UserId);
				model.BillingStatus = _billingStatusData.GetById(session.BillingStatusId);
				model.Rate = _rateData.GetByDate(session.Date, session.UserId, session.ProviderId);
				if (model.Rate == null)
				{
					model.Rate = new HourlyRate();
				}


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

			output.Client = _clientData.GetById(session.ClientId, session.UserId);
			output.Provider = _providerData.GetById(session.ProviderId, session.UserId);
			output.BillingStatus = _billingStatusData.GetById(session.BillingStatusId);
			output.Rate = _rateData.GetByDate(session.Date, session.UserId, session.ProviderId);
			if (output.Rate == null)
			{
				output.Rate = new HourlyRate();
			}

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

				model.Client = _clientData.GetById(session.ClientId, session.UserId);
				model.Provider = _providerData.GetById(session.ProviderId, session.UserId);
				model.BillingStatus = _billingStatusData.GetById(session.BillingStatusId);
				model.Rate = _rateData.GetByDate(session.Date, session.UserId, session.ProviderId);
				if (model.Rate == null)
				{
					model.Rate = new HourlyRate();
				}

				output.Add(model);
			}

			return output;
		}

		public int SaveSession(SessionModel model)
		{
			var dbModel = new SessionDbModel()
			{
				ClientId = model.Client.Id,
				UserId = model.UserId,
				Date = model.Date,
				StartTime = ConvertToTimeSpan(model.StartTime),
				EndTime = ConvertToTimeSpan(model.EndTime),
				ProviderId = model.Provider.Id,
				BillingStatusId = model.BillingStatus.Id,
				Notes = model.Notes
			};

			return _sql.SaveData("dbo.spSession_Insert", dbModel, "RevXData");
		}

		public void EditSession(SessionModel model)
		{
			var dbModel = new SessionDbModel()
			{
				Id = model.Id,
				UserId = model.UserId,
				ClientId = model.Client.Id,
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

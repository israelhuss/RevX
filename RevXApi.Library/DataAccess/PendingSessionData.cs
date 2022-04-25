using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class PendingSessionData : IPendingSessionData
	{
		private readonly ISqlDataAccess _sql;
		private readonly IClientData _clientData;
		private readonly IProviderData _providerData;

		public PendingSessionData(ISqlDataAccess sql, IClientData clientData, IProviderData providerData)
		{
			_sql = sql;
			_clientData = clientData;
			_providerData = providerData;
		}

		public void AddSchedule(ScheduleModel schedule, string userId)
		{
			List<DateOnly> allDates = ExplodeDates(schedule.DateFrom, schedule.DateTo);
			Dictionary<DayOfWeek, List<DateOnly>> dayDates = new();
			foreach (var date in allDates)
			{
				var day = date.DayOfWeek;
				if (schedule.DaysOfWeek.Contains((int)day))
				{
					if (dayDates.ContainsKey(day))
					{
						dayDates[ day ].Add(date);
					}
					else
					{
						dayDates.Add(day, new List<DateOnly>()
						{
							date,
						});
					}
				}
			}

			foreach (var date in dayDates)
			{
				_sql.StartTransaction("RevXData");
				try
				{
					var obj = new
					{
						UserId = userId,
						schedule.ClientId,
						schedule.ProviderId,
						DateFrom = schedule.DateFrom.ToShortDateString(),
						DateTo = schedule.DateTo.ToShortDateString(),
						StartTime = schedule.StartTime.ToShortTimeString(),
						EndTime = schedule.EndTime.ToShortTimeString(),
						DayOfWeek = date.Key
					};
					int planId = _sql.SaveDataInTransactionWithResult<int, dynamic>("dbo.spSessionPlan_Insert", obj).FirstOrDefault();
					if (planId != 0)
					{
						foreach (var day in date.Value)
						{
							var pendingObj = new
							{
								UserId = userId,
								Date = day.ToShortDateString(),
								PlanId = planId
							};
							int pendingId = _sql.SaveDataInTransactionWithResult<int, dynamic>("dbo.spPendingSession_Insert", pendingObj).FirstOrDefault();
							if (pendingId == 0)
							{
								throw new Exception();
							}
						}
					}
					else
					{
						throw new Exception();
					}
					_sql.CommitTransaction();
				}
				catch (Exception ex)
				{
					_sql.RollBackTransaction();
					throw;
				}
			}
		}

		public List<SessionModel> GetByMonth(int month, int year, string userId)
		{
			List<SessionDbModel> dbRes = _sql.LoadData<SessionDbModel, dynamic>("dbo.spPendingSession_GetByMonth", new { month, year, userId }, "RevXData");
			List<SessionModel> output = new();
			foreach (SessionDbModel db in dbRes)
			{
				SessionModel outputModel = new()
				{
					Id = db.Id,
					UserId = db.UserId,
					Date = db.Date,
					StartTime = db.StartTime.ToString(),
					EndTime = db.EndTime.ToString(),
					Notes = db.Notes,
				};
				outputModel.Client = _clientData.GetById(db.ClientId, userId);
				outputModel.Provider = _providerData.GetById(db.ClientId, userId);
				output.Add(outputModel);
			}
			return output;
		}

		public void Delete(int id, string userId)
		{
			_sql.SaveData("dbo.spPendingSession_Delete", new { Id = id, userId }, "RevXData");
		}

		private static List<DateOnly> ExplodeDates(DateOnly from, DateOnly to)
		{
			List<DateOnly> dates = new();
			while (true)
			{
				dates.Add(from);
				from = from.AddDays(1);
				if (from > to)
				{
					break;
				}
			}
			return dates;
		}
	}
}

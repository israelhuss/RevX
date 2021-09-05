using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevXApi.Library.DataAccess
{
	public class HourlyRateData : IHourlyRateData
	{
		private readonly ISqlDataAccess _sql;

		public HourlyRateData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<HourlyRate> GetAll(string userId)
		{
			return _sql.LoadData<HourlyRate, dynamic>("dbo.spRates_GetAll", new { userId }, "RevXData");
		}

		public void AddHourlyRate(HourlyRate rate)
		{
			_sql.SaveData("dbo.spRates_Insert", rate, "RevXData");
		}

		public HourlyRate GetByDate(DateTime date, string userId)
		{
			return _sql.LoadData<HourlyRate, dynamic>("dbo.spRates_GetByDate", new { Date = date, userId }, "RevXData").FirstOrDefault();
		}

		public void EditRate(HourlyRate model)
		{
			_sql.SaveData("dbo.spRates_Update", model, "RevXData");
		}
	}
}

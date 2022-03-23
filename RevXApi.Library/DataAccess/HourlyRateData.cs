using Microsoft.Extensions.Logging;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace RevXApi.Library.DataAccess
{
	public class HourlyRateData : IHourlyRateData
	{
		private readonly ISqlDataAccess _sql;
		private readonly ILogger<HourlyRateData> _logger;

		public HourlyRateData(ISqlDataAccess sql, ILogger<HourlyRateData> logger)
		{
			_sql = sql;
			_logger = logger;
		}

		public List<HourlyRate> GetAll(string userId, int providerId)
		{
			List<HourlyRate> result = _sql.LoadData<HourlyRate, dynamic>("dbo.spRates_GetAll", new { userId, providerId }, "RevXData");
			_logger.LogInformation("{0} requested hourly rates for provider {1} and the result was {2} long", userId, providerId, result.Count);
			return result;
		}

		public void AddHourlyRate(HourlyRate rate)
		{
            int res = _sql.SaveData("dbo.spRates_Insert", rate, "RevXData");
			_logger.LogInformation("{0} was added to the database, {1} rows were affected", JsonSerializer.Serialize(rate), res);
		}

		public HourlyRate GetByDate(DateTime date, string userId, int providerId)
		{
			var res = _sql.LoadData<HourlyRate, dynamic>("dbo.spRates_GetByDate", new { Date = date, userId, providerId }, "RevXData").FirstOrDefault();
			return res ?? new HourlyRate() { Rate = 0 };
		}

		public void EditRate(HourlyRate model)
		{
			_sql.SaveData("dbo.spRates_Update", model, "RevXData");
		}
	}
}

using RevXApi.Library.Models;
using System;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public class ReportData : IReportData
	{
		private readonly ISqlDataAccess _sql;

		public ReportData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public List<IncomeReportModel> GetMonthlyIncome(DateTime startDate, DateTime endDate, string userId, string groupBy)
		{
			var stuff = _sql.LoadData<IncomeReportModel, dynamic>("dbo.spReport_SchoolAndAfterSchool", new { startDate, endDate, userId, groupBy }, "RevXData");
			return stuff;
		}

	}
}

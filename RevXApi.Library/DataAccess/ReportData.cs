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

		public List<IncomeReportModel> GetMonthlyIncome(DateTime startDate)
		{
			var stuff = _sql.LoadData<IncomeReportModel, dynamic>("dbo.spReport_SchoolAndAfterSchool", new { StartDate = startDate }, "RevXData");
			return stuff;
		}

	}
}

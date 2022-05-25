using RevXApi.Library.Models;
using System;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IReportData
	{
		void Delete(int id, string userId);
		int EditReport(BarReportModel report);
		public List<IncomeReportModel> GetMonthlyIncome(DateTime startDate, DateTime endDate, string userId, string groupBy);
		List<IReportModel> GetReports(string userId);
		int SaveReport(BarReportModel report);
		void SetAsDefault(int id, string userId);
	}
}
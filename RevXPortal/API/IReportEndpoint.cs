using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IReportEndpoint
	{
		Task DeleteReport(int id);
		Task EditReport(BarReportModel report);
		Task<List<IReportModel>> GetAllReports();
		Task<List<IncomeReportModel>> GetMonthlyIncome(DateTime startDate, DateTime endDate, string groupBy);
		Task SaveReport(BarReportModel report);
		Task SetAsDefault(int id);
	}
}
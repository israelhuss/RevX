using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IReportEndpoint
	{
		Task DeleteReport(int id);
		Task DownloadReport(string filename);
        Task DownloadReport();
        Task EditReport(ReportModel report);
		Task<List<IReportModel>> GetAllReports();
		Task<List<IncomeReportModel>> GetMonthlyIncome(DateTime startDate, DateTime endDate, string groupBy);
		Task SaveReport(ReportModel report);
		Task SetAsDefault(int id);
	}
}
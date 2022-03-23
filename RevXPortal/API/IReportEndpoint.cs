using RevXPortal.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IReportEndpoint
	{
		Task<List<IncomeReportModel>> GetMonthlyIncome(DateTime startDate, DateTime endDate, string groupBy);
	}
}
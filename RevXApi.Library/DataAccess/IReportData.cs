using RevXApi.Library.Models;
using System;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IReportData
	{
		public List<IncomeReportModel> GetMonthlyIncome(DateTime startDate);
	}
}
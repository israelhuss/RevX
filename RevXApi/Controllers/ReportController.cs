using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly IReportData _reportData;

		public ReportController(IReportData reportData)
		{
			_reportData = reportData;
		}

		[HttpGet]
		public List<IncomeReportModel> GetAllStudents(DateTime startDate)
		{
			return _reportData.GetMonthlyIncome(startDate);
		}
	}
}

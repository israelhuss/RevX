﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ReportController : ControllerBase
	{
		private readonly IReportData _reportData;

		public ReportController(IReportData reportData)
		{
			_reportData = reportData;
		}

		[HttpGet]
		public List<IncomeReportModel> GetIncomeReports([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string groupBy = "month")
		{
			return _reportData.GetMonthlyIncome(startDate, endDate, User.FindFirstValue(ClaimTypes.NameIdentifier), groupBy);
		}
	}
}

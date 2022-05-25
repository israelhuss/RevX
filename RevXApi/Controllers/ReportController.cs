using Microsoft.AspNetCore.Authorization;
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
		public List<object> GetIncomeReports()
		{
			List<IReportModel> Reports = _reportData.GetReports(User.FindFirstValue(ClaimTypes.NameIdentifier));
			List<object> IncomeReports = new();
			foreach (var report in Reports)
			{
				IncomeReports.Add(report);
			}
			return IncomeReports;
		}

		[HttpGet("Old")]
		public List<IncomeReportModel> GetIncomeReports([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string groupBy = "month")
		{
			return _reportData.GetMonthlyIncome(startDate, endDate, User.FindFirstValue(ClaimTypes.NameIdentifier), groupBy);
		}

		[HttpPost]
		public IActionResult SaveReport(BarReportModel report)
		{
			report.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			int affected = _reportData.SaveReport(report);
			if (affected > 0)
			{
				return Created("/Report", report);
			}
			else
			{
				return StatusCode(500, "There was an error saving the report");
			}
		}

		[HttpPost]
		[Route("Delete/{id}")]
		public void Delete(int id)
		{
			_reportData.Delete(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
		}

		[HttpPost]
		[Route("SetDefault/{id}")]
		public IActionResult SetAsDefault(int id)
		{
			try
			{
				_reportData.SetAsDefault(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Something went wrong");
			}
		}

		[HttpPost]
		[Route("Edit")]
		public IActionResult EditReport(BarReportModel report)
		{
			try
			{
				report.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				int affected = _reportData.EditReport(report);
				if (affected > 0)
				{
					return Ok();
				}
				else
				{
					return StatusCode(500, "There was an error updating the report");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, "There was an error updating the report " + ex.Message);
			}
		}
	}
}

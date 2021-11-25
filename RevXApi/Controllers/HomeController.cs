using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using RevXApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RevXApi.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ISqlDataAccess _sql;

		public HomeController(ILogger<HomeController> logger, ISqlDataAccess sql)
		{
			_logger = logger;
			_sql = sql;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Invoice(int id)
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			InvoiceEmailModel model = _sql.LoadData<InvoiceEmailModel, dynamic>("spInvoice_Lookup", new { InvoiceId = id, UserId = userId }, "RevXData").FirstOrDefault();
			model.InvoiceSessions = _sql.Query<SessionEmailModel>($"SELECT * FROM Session WHERE InvoiceId = {id} AND UserId = '{userId}'", "RevXData");
			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

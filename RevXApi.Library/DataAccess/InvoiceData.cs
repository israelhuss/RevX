using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RazorEngine;
using RazorEngine.Templating;
using RevXApi.Library.Models;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class InvoiceData : IInvoiceData
	{
		private readonly ISqlDataAccess _sql;
		private readonly ISessionData _sessionData;
		private readonly IUserData _userData;
		private readonly IHourlyRateData _rateData;
		private readonly IWorkplaceData _workplaceData;
		private readonly ILogger<InvoiceData> _logger;
		private readonly IConfiguration _config;
		private readonly string _documentLocationRoot;

		public InvoiceData(ISqlDataAccess sql, ISessionData sessionData, IUserData userData, IHourlyRateData rateData, IWorkplaceData workplaceData, ILogger<InvoiceData> logger, IConfiguration config)
		{
			_sql = sql;
			_sessionData = sessionData;
			_userData = userData;
			_rateData = rateData;
			_workplaceData = workplaceData;
			_logger = logger;
			_config = config;
			_documentLocationRoot = _config[ "EmailConfig:DocumentTemplateLocationRoot" ];
		}


		public List<InvoiceModel> GetAll(string userId)
		{
			return _sql.LoadData<InvoiceModel, dynamic>("dbo.spInvoice_GetAll", new { userId }, "RevXData");
		}

		public int SaveInvoice(InvoiceModel invoice)
		{
			try
			{
				_sql.StartTransaction("RevXData");

				// Save the Invoice
				invoice.Id = _sql.SaveDataInTransactionWithResult<int, InvoiceDBModel>("dbo.spInvoice_Insert", new() { InvoiceDate = invoice.InvoiceDate, StartDate = invoice.StartDate, EndDate = invoice.EndDate, TotalHours = invoice.TotalHours, Rate = invoice.Rate, Total = invoice.Total, UserId = invoice.UserId, ProviderId = invoice.ProviderId }).FirstOrDefault();

				// 0 is Default for the int type
				if (invoice.Id > 0)
				{
					foreach (var id in invoice.SessionIds)
					{
						int affected = _sql.ExecuteCommandInTransaction<int>($"UPDATE Session SET BillingStatusId = {2}, InvoiceId = {invoice.Id} WHERE Id = {id} AND UserId = '{invoice.UserId}'");
					}

					_sql.CommitTransaction();
					return invoice.Id;
				}
				else
					throw new Exception("New invoice id was 0.");
			}
			catch
			{
				_sql.RollBackTransaction();
				throw;
			}
		}

		public InvoiceEmailModel PrepareEmailModel(InvoiceModel invoice)
		{
			// Fill in the basics
			InvoiceEmailModel output = new() { InvoiceDate = invoice.InvoiceDate, TotalHours = invoice.TotalHours, Rate = invoice.Rate, Signature = invoice.Signature, UserId = invoice.UserId };

			// Get the Name of employee
			UserModel employee = _userData.GetUserById(invoice.UserId);
			WorkplaceModel WorkplaceInfo = _workplaceData.GetCurrentUserInfo(employee.Id);
			string employeeName = $"{employee.FirstName} {employee.LastName}";
			output.FullName = employeeName;
			output.FromAddress = employee.EmailAddress;
			output.ToAddress = invoice.SentTo.Split(";").Select(s => new Address(s)).ToList();
			output.CC = invoice.CC?.Split(";").Select(s => new Address(s)).ToList();
			output.ClientNickname = WorkplaceInfo.ClientNickname;

			// Get the full session models, and change to email model
			output.InvoiceSessions = new();
			foreach (var item in invoice.SessionIds)
			{
				var temp = _sessionData.GetById(item, invoice.UserId);
				SessionEmailModel model = new()
				{
					Client = $"{temp.Client.FirstName} {temp.Client.LastName}",
					Date = temp.Date,
					StartTime = TimeSpan.Parse(temp.StartTime),
					EndTime = TimeSpan.Parse(temp.EndTime),
					Notes = temp.Notes
				};
				output.InvoiceSessions.Add(model);
			}

			// Calculate the invoicing period
			output.InvoicePeriod = CalculateInvoicePeriod(output, employee.BillingCycleId);

			return output;

		}

		public List<InvoiceModel> GenerateInvoicesFromSessions(List<SessionDbModel> sessions, string userId)
		{
			_logger.LogInformation(message: $"I got some sessions for {userId} to generate invoices", userId);
			Dictionary<string, List<SessionDbModel>> keyValuePairs = new();
			foreach (var session in sessions)
			{
				int month = session.Date.Month;
				int year = session.Date.Year;
				if (keyValuePairs.ContainsKey($"{month}-{year}-{session.ProviderId}"))
				{
					keyValuePairs.GetValueOrDefault($"{month}-{year}-{session.ProviderId}").Add(session);
				}
				else
				{
					keyValuePairs.Add($"{month}-{year}-{session.ProviderId}", new List<SessionDbModel> { session });
				}
			}
			List<InvoiceModel> output = new();
			foreach (List<SessionDbModel> month in keyValuePairs.Values)
			{
				month.Sort((s, x) => s.Date < x.Date ? -1 : 1);
				var invoice = new InvoiceModel() { UserId = userId };
				invoice.SessionIds = month.Select(x => x.Id).ToList();
				invoice.InvoiceDate = DateTime.Now;
				invoice.StartDate = month.First().Date;
				invoice.EndDate = month.Last().Date;
				invoice.Rate = _rateData.GetByDate(invoice.StartDate, invoice.UserId, month.First().ProviderId).Rate;
				invoice.ProviderId = month.First().ProviderId;
				invoice.TotalHours = month.Select(s => ( s.EndTime - s.StartTime ).TotalHours).Sum();
				invoice.Total = invoice.Rate * invoice.TotalHours;
				output.Add(invoice);
			}
			return output;
		}

		public byte[] GetDocument(int id, string userId)
		{
			InvoiceEmailModel model = _sql.LoadData<InvoiceEmailModel, dynamic>("spInvoice_Lookup", new { InvoiceId = id, UserId = userId }, "RevXData").FirstOrDefault();
			if (model == null)
			{
				return new byte[ 0 ];
			}
			model.InvoiceSessions = _sql.Query<SessionEmailModel>($"SELECT se.Id, CONCAT(cl.FirstName, ' ', cl.LastName) as Client, se.[Date], se.StartTime, se.EndTime, se.Notes FROM Session se JOIN Client cl ON se.ClientId = cl.Id WHERE se.InvoiceId = {id} AND se.UserId = '{userId}'", "RevXData");
			UserModel user = _userData.GetUserById(userId);
			model.InvoicePeriod = CalculateInvoicePeriod(model, user.BillingCycleId);
			model.FullName = user.FirstName + " " + user.LastName;
			string template = File.ReadAllText(_documentLocationRoot + "/InvoiceDocument.cshtml");
			string result = Engine.Razor.RunCompile(template, DateTime.Now.ToString(), null, model);
			// instantiate the html to pdf converter
			HtmlToPdf converter = new();
			converter.Options.MarginBottom = 30;
			converter.Options.MarginTop = 30;
			// convert the url to pdf
			PdfDocument doc = converter.ConvertHtmlString(result);

			// save pdf document
			var res = doc.Save();

			// close pdf document
			return res;
		}


		public byte[] GetDocument(int id, string userId, string signature)
		{
			InvoiceEmailModel model = _sql.LoadData<InvoiceEmailModel, dynamic>("spInvoice_Lookup", new { InvoiceId = id, UserId = userId }, "RevXData").FirstOrDefault();
			_logger.LogInformation("model was set to {0}", model.Id);
			model.InvoiceSessions = _sql.Query<SessionEmailModel>($"SELECT se.Id, CONCAT(cl.FirstName, ' ', cl.LastName) as Client, se.[Date], se.StartTime, se.EndTime, se.Notes FROM Session se JOIN Client cl ON se.ClientId = cl.Id WHERE se.InvoiceId = {id} AND se.UserId = '{userId}'", "RevXData");
			UserModel user = _userData.GetUserById(userId);
			model.InvoicePeriod = CalculateInvoicePeriod(model, user.BillingCycleId);
			model.Signature = signature;
			model.FullName = user.FirstName + " " + user.LastName;
			_logger.LogInformation("Trying to get the template from {0}", _documentLocationRoot + "/InvoiceDocumentSignature.cshtml");
			string template = File.ReadAllText(_documentLocationRoot + "/InvoiceDocumentSignature.cshtml");
			_logger.LogInformation("I think i got it");
			string result = Engine.Razor.RunCompile(template, DateTime.Now.ToString(), null, model);
			// instantiate the html to pdf converter
			HtmlToPdf converter = new();
			converter.Options.MarginBottom = 30;
			converter.Options.MarginTop = 30;
			// convert the url to pdf
			PdfDocument doc = converter.ConvertHtmlString(result);

			// save pdf document
			var res = doc.Save();

			// close pdf document
			return res;
		}

		private string CalculateInvoicePeriod(InvoiceEmailModel model, int billingCycleId)
		{
			if (billingCycleId == (int)BillingCycles.Monthly)
			{

				// Calculate the invoicing period
				List<string> tempMonth = new();
				List<string> tempYear = new();
				foreach (var item in model.InvoiceSessions)
				{
					tempMonth.Add(item.Date.ToString("MMMM"));
					tempYear.Add(item.Date.Year.ToString());
				}
				var monthString = String.Join(" - ", tempMonth.Distinct());
				var yearString = String.Join(" - ", tempYear.Distinct());

				string period = $"{monthString} {yearString}";
				return period;
			}
			else
			{
				model.InvoiceSessions.Sort((a, b) => a.Date.CompareTo(b.Date));
				var start = model.InvoiceSessions[ 0 ].Date;
				var end = model.InvoiceSessions[ model.InvoiceSessions.Count - 1 ].Date;
				if (start == end)
				{
					return start.ToShortDateString();
				}
				else
				{
					string period = $"{start.ToShortDateString()} - {end.ToShortDateString()}";
					return period;
				}
			}

		}

		private string Convert24HourTo12Hour(string twentyfour)
		{
			string output = string.Empty;
			var split = twentyfour.Split(":");
			if (split?.Length == 3)
			{
				if (int.Parse(split[ 0 ]) == 12)
				{
					output = $"12:{split[ 1 ]} PM";
				}
				else if (int.Parse(split[ 0 ]) == 0)
				{
					output = $"12:{split[ 1 ]} AM";
				}
				else if (int.Parse(split[ 0 ]) < 12)
				{
					output = $"{int.Parse(split[ 0 ])}:{split[ 1 ]} AM";
				}
				else if (int.Parse(split[ 0 ]) > 12)
				{
					output = $"{int.Parse(split[ 0 ]) - 12}:{split[ 1 ]} PM";
				}
			}
			return output;
		}
	}
}

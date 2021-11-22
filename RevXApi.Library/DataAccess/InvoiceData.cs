using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace RevXApi.Library.DataAccess
{
	public class InvoiceData : IInvoiceData
	{
		private readonly ISqlDataAccess _sql;
		private readonly ISessionData _sessionData;
		private readonly IUserData _userData;
		private readonly IHourlyRateData _rateData;

		public InvoiceData(ISqlDataAccess sql, ISessionData sessionData, IUserData userData, IHourlyRateData rateData)
		{
			_sql = sql;
			_sessionData = sessionData;
			_userData = userData;
			_rateData = rateData;
		}


		public List<InvoiceModel> GetAll(string userId)
		{
			return _sql.LoadData<InvoiceModel, dynamic>("dbo.spInvoice_GetAll", new { userId }, "RevXData");
		}

		public void SaveInvoice(InvoiceModel invoice)
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
			InvoiceEmailModel output = new() { InvoiceDate = invoice.InvoiceDate, TotalHours = invoice.TotalHours, Rate = invoice.Rate, Signature = invoice.Signature };

			// Get the Name of employee
			UserModel employee = _userData.GetUserById(invoice.UserId);
			string employeeName = $"{employee.FirstName} {employee.LastName}";
			output.FullName = employeeName;

			// Get the full session models, and change to email model
			output.InvoiceSessions = new();
			foreach (var item in invoice.SessionIds)
			{
				var temp = _sessionData.GetById(item, invoice.UserId);
				SessionEmailModel model = new()
				{
					Student = $"{temp.Student.FirstName} {temp.Student.LastName}",
					Date = temp.Date,
					StartTime = Convert24HourTo12Hour(temp.StartTime),
					EndTime = Convert24HourTo12Hour(temp.EndTime),
					Notes = temp.Notes
				};
				output.InvoiceSessions.Add(model);
			}

			// Calculate the invoicing period
			List<string> tempMonth = new();
			List<string> tempYear = new();
			foreach (var item in output.InvoiceSessions)
			{
				tempMonth.Add(item.Date.ToString("MMMM"));
				tempYear.Add(item.Date.Year.ToString());
			}
			var monthString = String.Join(" - ", tempMonth.Distinct());
			var yearString = String.Join(" - ", tempYear.Distinct());

			output.InvoicePeriod = $"{monthString} {yearString}";

			return output;

		}

		public List<InvoiceModel> GenerateInvoicesFromSessions(List<SessionDbModel> sessions)
		{
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
					keyValuePairs.Add($"{month}-{year}-{session.ProviderId}", new List<SessionDbModel> {session});
				}
			}
			List<InvoiceModel> output = new();
			foreach (var month in keyValuePairs.Values)
			{
				month.Sort((s, x) => s.Date < x.Date ? -1 : 1);
				var invoice = new InvoiceModel() { UserId = "d1e058a1-4da5-4a74-9ba2-1de0bba5460f" };
				invoice.SessionIds = month.Select(x => x.Id).ToList();
				invoice.InvoiceDate = DateTime.Now;
				invoice.StartDate = month.First().Date;
				invoice.EndDate = month.Last().Date;
				invoice.Rate = _rateData.GetByDate(invoice.StartDate, invoice.UserId, month.First().ProviderId).Rate;
				invoice.ProviderId = month.First().ProviderId;
				invoice.TotalHours = month.Select(s => (s.EndTime - s.StartTime).TotalHours).Sum();
				invoice.Total = invoice.Rate * invoice.TotalHours;
				output.Add(invoice);
			}
			return output;
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

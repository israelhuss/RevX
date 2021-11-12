using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevXApi.Library.DataAccess
{
	public class InvoiceData : IInvoiceData
	{
		private readonly ISqlDataAccess _sql;
		private readonly ISessionData _sessionData;
		private readonly IUserData _userData;

		public InvoiceData(ISqlDataAccess sql, ISessionData sessionData, IUserData userData)
		{
			_sql = sql;
			_sessionData = sessionData;
			_userData = userData;
		}

		public void SaveInvoice(InvoiceModel invoice)
		{
			try
			{
				_sql.StartTransaction("RevXData");

				// Save the Invoice
				_sql.SaveDataInTransaction("dbo.spInvoice_Insert", new { invoice.Id, invoice.InvoiceDate, invoice.TotalHours, invoice.UserId });

				// Get back the Id of this invoice
				invoice.Id = _sql.LoadDataInTransaction<int, dynamic>("dbo.spInvoice_Lookup", new { invoice.InvoiceDate, invoice.TotalHours, invoice.UserId }).FirstOrDefault();

				if (invoice.Id > 0)
				{
					foreach (var id in invoice.SessionIds)
					{
						var detail = new InvoiceDetailModel() { InvoiceId = invoice.Id, SessionId = id, UserId = invoice.UserId };

						// Save the Detail to database
						_sql.SaveDataInTransaction("dbo.spInvoiceDetail_Insert", detail);

						// Update the sessions billing status to invoiced
						// TODO - the status id should be looked up, not manually typed!!!!
						_sql.SaveDataInTransaction("dbo.spSession_EditBillingStatus", new { Id = id, StatusId = 2, UserId = invoice.UserId});
					}

					_sql.CommitTransaction();
				}
				else
					throw new Exception();
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

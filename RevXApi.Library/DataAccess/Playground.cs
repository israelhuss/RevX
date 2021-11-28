using Microsoft.Extensions.Logging;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class Playground : IPlayground
	{
		private readonly ISqlDataAccess _sql;
		private readonly IInvoiceData _invoiceData;
		private readonly ILogger<Playground> _logger;

		public Playground(ISqlDataAccess sql, IInvoiceData invoiceData, ILogger<Playground> logger)
		{
			_sql = sql;
			_invoiceData = invoiceData;
			_logger = logger;
		}

		public string Play(string userId)
		{
			var allDetails = _sql.Query<SessionDbModel>($"Select * From Session Where InvoiceId is null AND UserId = '{userId}' AND BillingStatusId > 1", "RevXData");
			if (allDetails.Count > 0)
			{
				var invoices = _invoiceData.GenerateInvoicesFromSessions(allDetails, userId);
				foreach (var invoice in invoices)
				{
					try
					{
						_invoiceData.SaveInvoice(invoice);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}
				}
				return $"allDetails was {allDetails.Count} long, I'm done";
			}
			else
			{
				return "I didnt get any results from the database, sql query was" + $"Select * From Session Where InvoiceId is null AND UserId = '{userId}'";
			}
		}
	}
}

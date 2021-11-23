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

		public Playground(ISqlDataAccess sql, IInvoiceData invoiceData)
		{
			_sql = sql;
			_invoiceData = invoiceData;
		}

		public void Play(string userId)
		{
			var allDetails = _sql.Query<SessionDbModel>($"Select * From Session Where InvoiceId is null AND UserId = '{userId}'", "RevXData");
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
		}
	}
}

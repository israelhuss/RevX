using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class InvoiceData : IInvoiceData
	{
		private readonly ISqlDataAccess _sql;

		public InvoiceData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public async void SaveInvoice(InvoiceModel invoice)
		{
			try
			{
				_sql.StartTransaction("RevXData");

				// Save the Invoice
				await _sql.SaveDataInTransaction("dbo.spInvoice_Insert", new { invoice.Id, invoice.InvoiceDate, invoice.TotalHours });

				// Get back the Id of this invoice
				invoice.Id = _sql.LoadDataInTransaction<int, dynamic>("dbo.spInvoice_Lookup", new { invoice.InvoiceDate, invoice.TotalHours }).FirstOrDefault();

				foreach (var id in invoice.SessionIds)
				{
					var detail = new InvoiceDetailModel() { InvoiceId = invoice.Id, SessionId = id};

					// Save the Detail to database
					await _sql.SaveDataInTransaction("dbo.spInvoiceDetail_Insert", detail);
				}

				_sql.CommitTransaction();
			}
			catch
			{
				_sql.RollBackTransaction();
				throw;
			}
		}
	}
}

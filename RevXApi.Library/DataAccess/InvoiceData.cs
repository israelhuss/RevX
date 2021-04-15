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

		public void SaveInvoice(InvoiceModel invoice)
		{
			try
			{
				_sql.StartTransaction("RevXData");

				// Save the Invoice
				_sql.SaveDataInTransaction("dbo.spInvoice_Insert", invoice);

				// Get back the Id of this invoice
				invoice.Id = _sql.LoadDataInTransaction<int, dynamic>("dbo.spInvoice_Lookup", new { invoice.InvoiceDate, invoice.TotalHours }).FirstOrDefault();

				foreach (var s in invoice.InvoiceDetails)
				{
					s.InvoiceId = invoice.Id;

					// Save the Detail to database
					_sql.SaveDataInTransaction("dbo.spInvoiceDetail_Insert", s);
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

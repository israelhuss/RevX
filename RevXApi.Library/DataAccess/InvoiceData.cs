using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public class InvoiceData
	{
		private readonly ISqlDataAccess _sql;

		public InvoiceData(ISqlDataAccess sql)
		{
			_sql = sql;
		}

		public void SaveInvoice(List<int> sessionIds)
		{

		}
	}
}

using RevXApi.Library.Models;
using System.Collections.Generic;

namespace RevXApi.Library.DataAccess
{
	public interface IInvoiceData
	{
		void SaveInvoice(InvoiceModel invoice);
		InvoiceEmailModel PrepareEmailModel(InvoiceModel invoice);
		List<InvoiceModel> GenerateInvoicesFromSessions(List<SessionDbModel> sessions);
		List<InvoiceModel> GetAll(string userId);
	}
}
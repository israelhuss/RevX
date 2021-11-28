using RevXApi.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXApi.Library.DataAccess
{
	public interface IInvoiceData
	{
		int SaveInvoice(InvoiceModel invoice);
		InvoiceEmailModel PrepareEmailModel(InvoiceModel invoice);
		List<InvoiceModel> GenerateInvoicesFromSessions(List<SessionDbModel> sessions, string userId);
		List<InvoiceModel> GetAll(string userId);
		byte[] GetDocument(int id, string userId);
		byte[] GetDocument(int id, string userId, string signature);
	}
}
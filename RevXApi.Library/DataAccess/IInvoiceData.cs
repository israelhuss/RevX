using RevXApi.Library.Models;

namespace RevXApi.Library.DataAccess
{
	public interface IInvoiceData
	{
		void SaveInvoice(InvoiceModel invoice);
		InvoiceEmailModel PrepareEmailModel(InvoiceModel invoice);
	}
}
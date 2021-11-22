using RevXPortal.Models;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IInvoiceEndpoint
	{
		Task<List<InvoiceModel>> GetAll();
		Task SaveInvoice(InvoiceModel invoice);
	}
}
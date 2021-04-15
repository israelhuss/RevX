using RevXPortal.Models;
using System.Threading.Tasks;

namespace RevXPortal.API
{
	public interface IInvoiceEndpoint
	{
		Task SaveInvoice(InvoiceModel invoice);
	}
}
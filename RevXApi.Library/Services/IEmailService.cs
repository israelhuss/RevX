using RevXApi.Library.Models;
using System.Threading.Tasks;

namespace RevXApi.Library.Services
{
	public interface IEmailService
	{
		Task SendEmail();
		Task SendInvoiceEmail(string emailAddress, InvoiceEmailModel emailModel);
	}
}
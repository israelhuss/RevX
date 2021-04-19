using FluentEmail.Core.Models;
using RevXApi.Library.Models;
using System.Threading.Tasks;

namespace RevXApi.Library.Services
{
	public interface IEmailService
	{
		Task SendEmail();
		Task<SendResponse> SendInvoiceEmail(string emailAddress, InvoiceEmailModel emailModel);
	}
}
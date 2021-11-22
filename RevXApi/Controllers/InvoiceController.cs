using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using RevXApi.Library.Services;
using System.Collections.Generic;
using System.Security.Claims;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoiceController : ControllerBase
	{
		private readonly IEmailService _emailService;
		private readonly IInvoiceData _invoiceData;
		private readonly IConfiguration _config;

		public InvoiceController(IEmailService emailService, IInvoiceData invoiceData, IConfiguration config)
		{
			_emailService = emailService;
			_invoiceData = invoiceData;
			_config = config;
		}

		[HttpGet]
		public List<InvoiceModel> GetAll(string userId)
		{
			userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			return _invoiceData.GetAll(userId);
		}

		[HttpPost]
		[Route("TestEmail")]
		public async void SendEmail()
		{
			await _emailService.SendEmail();
		}

		[HttpPost]
		public IActionResult SaveInvoice(InvoiceModel invoice)
		{
			InvoiceEmailModel emailModel = _invoiceData.PrepareEmailModel(invoice);
			_invoiceData.SaveInvoice(invoice);
			var status = _emailService.SendInvoiceEmail(_config["EmailConfig:SecretaryEmail"], emailModel);
			if (status.Exception is null)
			{
				return Ok();
			}
			else if (status.Exception is not null)
			{
				System.Console.WriteLine(status.Exception);
				return StatusCode(500);
			}
			else
			{
				return StatusCode(500);
			}
		}
	}
}

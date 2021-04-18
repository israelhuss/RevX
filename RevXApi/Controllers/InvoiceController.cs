using FluentEmail.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using RevXApi.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoiceController : ControllerBase
	{
		private readonly IEmailService _emailService;
		private readonly IInvoiceData _invoiceData;

		public InvoiceController(IEmailService emailService, IInvoiceData invoiceData)
		{
			_emailService = emailService;
			_invoiceData = invoiceData;
		}

		[HttpPost]
		[Route("SendEmail")]
		public async void SendEmail()
		{
			await _emailService.SendEmail();
		}

		[HttpPost]
		public IActionResult SaveInvoice(InvoiceModel invoice)
		{
			InvoiceEmailModel emailModel = _invoiceData.PrepareEmailModel(invoice);
			_invoiceData.SaveInvoice(invoice);
			_emailService.SendInvoiceEmail("israelmhuss@gmail.com", emailModel);
			return Ok();
		}
	}
}

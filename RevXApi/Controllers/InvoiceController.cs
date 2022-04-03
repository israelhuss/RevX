using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using RazorEngine;
using RazorEngine.Templating;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using RevXApi.Library.Services;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class InvoiceController : ControllerBase
	{
		private readonly IEmailService _emailService;
		private readonly IInvoiceData _invoiceData;

		public InvoiceController(IEmailService emailService, IInvoiceData invoiceData, IConfiguration config)
		{
			_emailService = emailService;
			_invoiceData = invoiceData;
		}

		[HttpGet]
		public List<InvoiceModel> GetAll()
		{
			return _invoiceData.GetAll(User.FindFirstValue(ClaimTypes.NameIdentifier));
		}

		[HttpPost]
		[Route("TestEmail")]
		[AllowAnonymous]
		public async void SendEmail()
		{
			await _emailService.SendEmail();
		}

		[HttpPost]
		public IActionResult SaveInvoice(InvoiceModel invoice)
		{
			invoice.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			InvoiceEmailModel emailModel = _invoiceData.PrepareEmailModel(invoice);
			emailModel.Id = _invoiceData.SaveInvoice(invoice);
			var status = _emailService.SendInvoiceEmail(emailModel);
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

		[HttpGet]
		[Route("document")]
		public FileContentResult GetDocument(int id)
		{
			byte[] res = _invoiceData.GetDocument(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
			return File(res, "application/pdf", "test.pdf");
		}

		[HttpGet]
		[Route("bytes")]
		public IActionResult GetDocumentBytes(int id)
		{
			var res = _invoiceData.GetDocument(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
			return Ok(res);
		}
	}
}

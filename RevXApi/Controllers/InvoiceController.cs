using FluentEmail.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevXApi.Library.Models;
using RevXApi.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevXApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoiceController : ControllerBase
	{
		private readonly IEmailService _emailService;

		public InvoiceController(IEmailService emailService)
		{
			_emailService = emailService;
		}

		[HttpPost]
		[Route("SendEmail")]
		public async void SendEmail()
		{
			await _emailService.SendEmail();
		}

		[HttpPost]
		public void SaveInvoice(List<int> sessionIds)
		{

		}
	}
}

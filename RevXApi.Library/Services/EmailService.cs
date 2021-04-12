using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RevXApi.Library.Services
{
	public class EmailService : IEmailService
	{
		private readonly IFluentEmail _fluentEmail;
		private readonly IConfiguration _config;

		public EmailService(IFluentEmail fluentEmail, IConfiguration config)
		{
			_fluentEmail = fluentEmail;
			_config = config;
		}
		public async Task SendEmail()
		{
			//StringBuilder template = new();
			//template.AppendLine("Dear @Model")


			var email = await _fluentEmail
				.To(_config["EmailConfig:IsraelEmailAddress"], "Israel Huss")
				.Subject("Hi from the RevX Team")
				.UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/../RevXApi.Library/EmailTemplates/InvoiceTemplate.cshtml", new { Name = "Israel" })
				//.Body("This is a email confirming that if the body is different.")
				//.UsingTemplate("Hi @Model.Name, greetings from a template.", new { Name = "Israel" })
				.SendAsync();
		}
	}
}

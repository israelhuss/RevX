using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
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
			List<SessionModel> sessions = new();
			sessions.Add(new SessionModel() { Student = new StudentModel() { FirstName = "David", LastName = "Schwartz" }, Date = DateTime.Now, StartTime = "19:56:00", EndTime = "21:22:00", Notes = "Hello In the email." });
			sessions.Add(new SessionModel() { Student = new StudentModel() { FirstName = "Mendy", LastName = "Surkis" }, Date = DateTime.Now.AddDays(-2), StartTime = "01:56:00", EndTime = "02:22:00", Notes = "second one." });
			sessions.Add(new SessionModel() { Student = new StudentModel() { FirstName = "David", LastName = "Schwartz" }, Date = DateTime.Now, StartTime = "19:56:00", EndTime = "21:22:00", Notes = "Hello In the email." });
			sessions.Add(new SessionModel() { Student = new StudentModel() { FirstName = "David", LastName = "Schwartz" }, Date = DateTime.Now, StartTime = "19:56:00", EndTime = "21:22:00", Notes = "Hello In the email." });

			var email = await _fluentEmail
				.To(_config["EmailConfig:IsraelEmailAddress"], "Israel Huss")
				.Subject("Hi from the RevX Team")
				.UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/../RevXApi.Library/EmailTemplates/InvoiceTemplate.cshtml", new { Name = "Israel", InvoiceSessions = sessions, TotalHours = 79, InvoicePeriod = "Hello" }, true)
				//.Body("This is a email confirming that if the body is different.")
				//.UsingTemplate("Hi @Model.Name, greetings from a template.", new { Name = "Israel" })
				//.UsingTemplateFromEmbedded("RevXApi.Library.InvoiceTemplate.cshtml", new { Name = "Israel", Sessions = sessions }, Assembly.Load("RevXApi.Library"))
				.SendAsync();
		}

		public async Task<SendResponse> SendInvoiceEmail(string emailAddress, InvoiceEmailModel emailModel)
		{
			var email = await _fluentEmail
				.To(emailAddress)
				.Subject($"Faigy Huss {emailModel.InvoicePeriod} Hours")
				.UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/../RevXApi.Library/EmailTemplates/InvoiceTemplate.cshtml", emailModel, true)
				.SendAsync();
			return email;
		}
	}
}

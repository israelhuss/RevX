using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Configuration;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevXApi.Library.Services
{
	public class EmailService : IEmailService
	{
		private readonly IFluentEmail _fluentEmail;
		private readonly IConfiguration _config;
		private readonly string _templateLocation;

		public EmailService(IFluentEmail fluentEmail, IConfiguration config)
		{
			_fluentEmail = fluentEmail;
			_config = config;
			_templateLocation = _config["EmailConfig:TemplateLocation"];
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
				.UsingTemplateFromFile(_templateLocation, new { Name = "Israel", InvoiceSessions = sessions, TotalHours = 79, InvoicePeriod = "Hello" }, true)
				.SendAsync();
		}

		public async Task<SendResponse> SendInvoiceEmail(string emailAddress, InvoiceEmailModel emailModel)
		{
			var email = await _fluentEmail
					.To(emailAddress)
					.Subject($"Faigy Huss {emailModel.InvoicePeriod} Hours")
					.UsingTemplateFromFile(_templateLocation, emailModel, true)
					.SendAsync();
			if (email.ErrorMessages.Count > 1)
			{
				Console.WriteLine(email.ErrorMessages);
			}
			return email;
		}
	}
}
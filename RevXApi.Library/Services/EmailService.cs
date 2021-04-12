using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
			var email = await _fluentEmail
				.To(_config["EmailConfig:IsraelEmailAddress"], "Israel Huss")
				.Subject("Hi from the RevX Team")
				.Body("This is a email confirming that if the body is different.")
				.SendAsync();
		}
	}
}

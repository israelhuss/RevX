using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using RazorLight.Extensions;
using RevXApi.Library.DataAccess;
using RevXApi.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RevXApi.Library.Services
{
	public class EmailService : IEmailService
	{
		private readonly IFluentEmail _fluentEmail;
		private readonly IConfiguration _config;
		private readonly IInvoiceData _invoiceData;
		private readonly string _templateLocation;

		public EmailService(IFluentEmail fluentEmail, IConfiguration config, IInvoiceData invoiceData)
		{
			_fluentEmail = fluentEmail;
			_config = config;
			_invoiceData = invoiceData;
			_templateLocation = _config["EmailConfig:TemplateLocation"];
		}
		public async Task SendEmail()
		{
			List<SessionModel> sessions = new();
			sessions.Add(new SessionModel() { Student = new StudentModel() { FirstName = "David", LastName = "Schwartz" }, Date = DateTime.Now, StartTime = "19:56:00", EndTime = "21:22:00", Notes = "Hello In the email." });
			sessions.Add(new SessionModel() { Student = new StudentModel() { FirstName = "Mendy", LastName = "Surkis" }, Date = DateTime.Now.AddDays(-2), StartTime = "01:56:00", EndTime = "02:22:00", Notes = "second one." });
			sessions.Add(new SessionModel() { Student = new StudentModel() { FirstName = "David", LastName = "Schwartz" }, Date = DateTime.Now, StartTime = "19:56:00", EndTime = "21:22:00", Notes = "Hello In the email." });
			sessions.Add(new SessionModel() { Student = new StudentModel() { FirstName = "David", LastName = "Schwartz" }, Date = DateTime.Now, StartTime = "19:56:00", EndTime = "21:22:00", Notes = "Hello In the email." });

			string signature = "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";

			//byte[] byteArray = Encoding.UTF8.GetBytes(signature);
			//byte[] byteArray = Encoding.ASCII.GetBytes(signature);
			byte[] documentBytes = _invoiceData.GetDocument(4034, "d1e058a1-4da5-4a74-9ba2-1de0bba5460f");
			byte[] bytes = Convert.FromBase64String(signature);

			MemoryStream stream = new(bytes);
			MemoryStream document = new(documentBytes);

			var email = await _fluentEmail
				.To(_config["EmailConfig:IsraelEmailAddress"], "Israel Huss")
				.Subject("Hi from the RevX Team")
				.Attach(new Attachment() { IsInline = true, ContentId = "Signature", Data = stream, ContentType = "image/png", Filename = "Signature.png" })
				.Attach(new Attachment() { ContentId = "Document", ContentType = "application/pdf", Data = document, Filename = "hello.pdf"})
				.UsingTemplateFromFile(_templateLocation, new { Name = "Israel", InvoiceSessions = sessions, TotalHours = 79, InvoicePeriod = "Hello", Rate = 32, Signature = signature }, true)
				.SendAsync();
		}

		public async Task<SendResponse> SendInvoiceEmail(string emailAddress, InvoiceEmailModel emailModel)
		{
			SendResponse email;
			if (emailModel.Signature != null)
			{
				byte[] documentBytes = _invoiceData.GetDocument(emailModel.Id, emailModel.UserId, "data:image/png;base64," + emailModel.Signature.Replace("data:image/png;base64,", ""));
				MemoryStream document = new(documentBytes);
				string signature = emailModel.Signature.Replace("data:image/png;base64,", "");
				byte[] bytes = Convert.FromBase64String(signature);
				MemoryStream stream = new(bytes);
				email = await _fluentEmail
					.To(emailAddress)
					.Subject($"{emailModel.FullName} {emailModel.InvoicePeriod} Hours")
					.Attach(new Attachment() { IsInline = true, ContentId = "Signature", Data = stream, ContentType = "image/png", Filename = "Signature.png" })
					.Attach(new Attachment() { ContentId = "Document", ContentType = "application/pdf", Data = document, Filename = $"{emailModel.FullName} {emailModel.InvoicePeriod}.pdf" })
					.UsingTemplateFromFile(_templateLocation, emailModel, true)
					.SendAsync();
			} else
			{
				byte[] documentBytes = _invoiceData.GetDocument(emailModel.Id, emailModel.UserId);
				MemoryStream document = new(documentBytes);
				email = await _fluentEmail
						.To(emailAddress)
						.Subject($"{emailModel.FullName} {emailModel.InvoicePeriod} Hours")
						.Attach(new Attachment() { ContentId = "Document", ContentType = "application/pdf", Data = document, Filename = $"{emailModel.FullName} {emailModel.InvoicePeriod}.pdf" })
						.UsingTemplateFromFile(_templateLocation, emailModel, true)
						.SendAsync();
			}
			if (email.ErrorMessages.Count > 1)
			{
				Console.WriteLine(email.ErrorMessages);
			}
			return email;
		}
	}
}
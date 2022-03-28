using FluentEmail.Core.Models;
using System;
using System.Collections.Generic;

namespace RevXApi.Library.Models
{
	public class InvoiceEmailModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string FullName { get; set; }
		public string FromAddress { get; set; }
		public List<Address> ToAddress { get; set; }
		public List<Address> CC { get; set; }
		public DateTime InvoiceDate { get; set; }
		public double TotalHours { get; set; }
		public double Rate { get; set; }
		public List<SessionEmailModel> InvoiceSessions { get; set; }
		public string InvoicePeriod { get; set; }
		public string Signature { get; set; }
		public string ClientNickname { get; set; }
	}
}

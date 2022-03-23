using System;

namespace RevXPortal.Models
{
	public class SessionApiModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public StudentModel Student { get; set; }
		public DateTime Date { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public ProviderModel Provider { get; set; }
		public BillingStatusModel BillingStatus { get; set; }
		public string Notes { get; set; }
		public HourlyRate Rate { get; set; }
		public int InvoiceId { get; set; }
	}

}

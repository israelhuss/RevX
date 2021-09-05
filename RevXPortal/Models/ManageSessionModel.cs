using RevXPortal.FormValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace RevXPortal.Models
{
	[NotBeforeStartTime]
	[Duration(12)]
	public class ManageSessionModel
	{

		public int Id { get; set; }
		public string UserId { get; set; }
		[Required]
		public StudentModel Student { get; set; }
		[Required]
		[NotAfterToday]
		public DateTime Date { get; set; } = DateTime.Today;
		[NotNegative]
		public TimeSpan StartTime { get; set; } = new(-1);
		[NotNegative]
		public TimeSpan EndTime { get; set; } = new(-1);
		[Required]
		public ProviderModel Provider { get; set; }
		[Required]
		public BillingStatusModel BillingStatus { get; set; }

		public string Notes { get; set; }
	}
}

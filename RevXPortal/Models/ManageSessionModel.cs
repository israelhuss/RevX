using RevXPortal.Converters;
using RevXPortal.FormValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.Models
{
	[NotBeforeStartTime]
	[Duration(12)]
	public class ManageSessionModel
	{
		public int Id { get; set; }
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

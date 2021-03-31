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
		[NotNull]
		public StudentModel Student { get; set; }
		[Required]
		[NotAfterToday]
		public DateTime Date { get; set; } = DateTime.Today;
		[Required]
		public string StartTime { get; set; }
		[Required]
		public string EndTime { get; set; }
		[Required]
		public string Provider { get; set; }
		[Required]
		public string BillingStatus { get; set; }
		
		public string Notes { get; set; }
	}
}

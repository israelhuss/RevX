using RevXPortal.FormValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.Models
{
	[NotBeforeStartTime]
	[Duration(12)]
	public class FormSessionModel
	{
		[Required]
		[RegularExpression("Duvi Schwartz|Mendy Surkis|Shaya Spitzer", ErrorMessage = "Please select a Student.")]
		public string Student { get; set; }
		[Required]
		[NotAfterToday]
		public DateTime Date { get; set; }
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

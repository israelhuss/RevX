using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.Models
{
	public class DisplaySessionModel
	{
		public string Student { get; set; }
		public DateTime Date { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public string Provider { get; set; }
		public string BillingStatus { get; set; }
	}
}

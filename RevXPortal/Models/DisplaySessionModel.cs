﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevXPortal.Models
{
	public class DisplaySessionModel
	{
		public StudentModel Student { get; set; }
		public DateTime Date { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public ProviderModel Provider { get; set; }
		public BillingStatusModel BillingStatus { get; set; }
		public string Notes { get; set; }
	}
}

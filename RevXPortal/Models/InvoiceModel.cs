using System;
using System.Collections.Generic;

namespace RevXPortal.Models
{
	public class InvoiceModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime InvoiceDate { get; set; } = DateTime.Now;
		public List<int> SessionIds { get; set; }
		public double TotalHours { get; set; }
	}
}

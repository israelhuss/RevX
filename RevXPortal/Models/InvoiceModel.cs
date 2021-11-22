using System;
using System.Collections.Generic;

namespace RevXPortal.Models
{
	public class InvoiceModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime InvoiceDate { get; set; } = DateTime.Now;
		public int ProviderId { get; set; }
		public List<int> SessionIds { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public double TotalHours { get; set; }
		public double Rate { get; set; }
		public double Total { get; set; }
		public string Signature { get; set; }
	}
}

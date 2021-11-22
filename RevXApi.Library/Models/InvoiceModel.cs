using System;
using System.Collections.Generic;

namespace RevXApi.Library.Models
{
	public class InvoiceModel
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime InvoiceDate { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public double TotalHours { get; set; }
		public double Rate { get; set; }
		public double Total { get; set; }
		public List<int> SessionIds { get; set; }
		public int ProviderId { get; set; }
		public string Signature { get; set; }
	}
}
